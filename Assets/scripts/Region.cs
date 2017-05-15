using UnityEngine;
using System.Collections;
using System;

public class Region : MonoBehaviour {

	public bool selected;
	public bool isNazi;
	public Sprite republicanRegionSprite;
	public Sprite naziRegionSprite;
	public bool enabledRegion;
	public RegionType regionType;
	public int actionGenerationLevel = 0;
	public int militaryLevel = 0;

	private SpriteRenderer spriteRenderer;
	
	public RegionArmySlot[] armySlots;
	public RegionArmySlot[] GetArmySlots(){
		return armySlots;
	}
	private SpiedRegionInfo spiedRegionInfo;

	void Awake(){
		// Restore all values from a previous game
		int maxSlots = GetMaxSlots ();
		armySlots = new RegionArmySlot[maxSlots];
		ClearArmySlots ();
		spriteRenderer = GetComponent<SpriteRenderer>();
		FindObjectOfType<GameManager> ().AddRegionToList(this);

		if(isNazi){
			AddUnitsToArmy (ArmyType.NaziTroop, 50);
			AddUnitsToArmy (ArmyType.NaziFlak, 30);
		}
			
		if(regionType == RegionType.Asturias){
			AddUnitsToArmy (ArmyType.Antiaereo, 100);
			AddUnitsToArmy (ArmyType.Milicia, 300);
			//AddUnitsToArmy (ArmyType.TankBisonte, 6);
			//AddUnitsToArmy (ArmyType.TankToro, 44);
			//AddUnitsToArmy (ArmyType.FighterBomberHalcon, 55);
			//AddUnitsToArmy (ArmyType.BomberQuebrantahuesos, 55);
		}
		if(regionType == RegionType.Andalucia){
			AddUnitsToArmy (ArmyType.TankToro, 66);
		}
		if(regionType == RegionType.Valencia){
			AddUnitsToArmy (ArmyType.TankToro, 69);
			AddUnitsToArmy (ArmyType.Antiaereo, 34);
		}

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int GetMaxSlots(){
		return (FindObjectOfType<ArmyManager> ()).MAX_ARMY_SLOTS_PER_REGION;
	}

	public void toggleSelected(){
		selected = !selected;
		Animator animator = GetComponent<Animator>();
		animator.SetBool ("selected", selected);


		GameManager gameManager = FindObjectOfType<GameManager>();

		if(selected){
			gameManager.RegionSelected(this);
		}
		else{
			gameManager.RegionUnselected();
		}
	}

	public bool isSelected(){
		return selected;
	}

	public bool IsEnabled(){
		return enabledRegion;
	}

	public void SetNaziConquered(bool naziConquered){
		isNazi = naziConquered;
		
		RefreshSprite ();
	}

	public void RefreshSprite(){
		if(isNazi){
			spriteRenderer.sprite = naziRegionSprite;
		}
		else{
			spriteRenderer.sprite = republicanRegionSprite;
		}
	}

	public RegionArmySlot GetArmySlotOfType(ArmyType type){
		foreach(RegionArmySlot armySlot in armySlots){
			if(type == armySlot.armyType){
				return armySlot;
			}
		}

		return null;
	}

	public void AddUnitsToArmy(ArmyType type, int units){

		// Check if this region has the type already.
		RegionArmySlot chosenSlot = GetArmySlotOfType(type);
		if (chosenSlot != null) {
			chosenSlot.addUnits (units);
		} 
		else {
			// We haven't recruited this type yet. Just add units to the next non-used slot
			foreach(RegionArmySlot nextSlot in armySlots){
				if(nextSlot.armyType == ArmyType.Empty){
					chosenSlot = nextSlot;
					chosenSlot.armyType = (ArmyType)type;
					chosenSlot.armyAmount = units;
					break;
				}
			}
		}

		if(chosenSlot == null){
			Debug.Log ("Region: AddUnitsToArmy => WARNING : Trying to add " + units + " of type " + type +
				" but no slots available !!");
		}

	}

	public void ClearArmySlots(){
		for(int i=0; i<armySlots.Length; i++){
			if(armySlots[i] == null){
				armySlots[i] = new RegionArmySlot();
			}
			armySlots[i].armyType = ArmyType.Empty;
			armySlots[i].armyAmount = 0;
		}
	}

	public bool IsThereAnEmptySlot(){
		foreach(RegionArmySlot armySlot in armySlots){
			if(armySlot.armyType == ArmyType.Empty){
				return true;	
			}
		}

		return false;
	}

	public bool HasAnyTroops(){
		foreach(RegionArmySlot armySlot in armySlots){
			if(armySlot.armyType != ArmyType.Empty){
				return true;
			}
		}

		return false;
	}

	public bool HasFullSlots(){
		foreach(RegionArmySlot armySlot in armySlots){
			if(armySlot.armyType == ArmyType.Empty){
				return false;
			}
		}

		return true;
	}

	public bool HasTroopOfType(ArmyType armyType){
		foreach(RegionArmySlot armySlot in armySlots){
			if(armySlot.armyType == armyType){
				return true;
			}
		}

		return false;
	}

	/**
	 * Clean any empty slot, moving to it the next non-empty slot
	 **/
	public void SortTroopSlots(){
		for(int i=0; i<armySlots.Length; i++){
			// Look for the first empty army slot
			if(armySlots[i].armyType == ArmyType.Empty){
				for(int j=i+1; j<armySlots.Length; j++){
					if(armySlots[j].armyType != ArmyType.Empty){
						armySlots[i].armyType = armySlots[j].armyType;
						armySlots[i].armyAmount = armySlots[j].armyAmount;
						armySlots[j].CleanSlot();
						break;
					}
				}
			}
		}
	}

	public int GetActionGenerationPoints(){
		BuildValues buildValues = FindObjectOfType<BuildValues> ();
		int actionGenerationPoints = 0;

		for(int i=0; i<buildValues.actionBuildingsList.Length; i++){
			// If this building is built in this region, add its action generation points to the total
			if(actionGenerationLevel > i){
				actionGenerationPoints += buildValues.actionGenerationPointsPerBuilding[i];
			}
		}

		return actionGenerationPoints;
	}

	public int GetActionGenerationLevel(){
		return actionGenerationLevel;
	}

	public void IncreaseActionGenerationLevel(){
		actionGenerationLevel++;
	}

	public int GetMilitaryPointsGeneration(){
		BuildValues buildValues = FindObjectOfType<BuildValues> ();
		int militaryGenerationPoints = 0;

		for(int i=0; i<buildValues.militaryBuildingsList.Length; i++){
			// If this building is built in this region, add its action generation points to the total
			if(militaryLevel > i){
				militaryGenerationPoints += buildValues.militaryPointsPerBuilding[i];
			}
		}

		return militaryGenerationPoints;
	}

	public int GetMilitaryLevel(){
		return militaryLevel;
	}

	public void IncreaseMilitaryLevel(){
		militaryLevel++;
	}

	public void RecalculateSpiedInfo(float chanceOfFindingArmyType, float chanceOfFindingArmyAmount){
		spiedRegionInfo = ScriptableObject.CreateInstance<SpiedRegionInfo> ();
		spiedRegionInfo.Initialize(this, chanceOfFindingArmyType, chanceOfFindingArmyAmount);
	}

	public SpiedRegionInfo GetLastSpiedRegionInfo(){
		return spiedRegionInfo;
	}

	public bool IsBeingSpied(){
		return FindObjectOfType<SpyManager> ().IsAnySpyInRegion (this);
	}

	public void Enable(){
		enabledRegion = true;
		RefreshSprite ();
		spriteRenderer.material = UIManager.GetDefaultMaterial();
	}

	public void Disable(){
		enabledRegion = false;
		RefreshSprite ();
		spriteRenderer.material = UIManager.GetDisabledMaterial();
	}

	public Sprite GetCurrentSprite(){
		if (isNazi) {
			return naziRegionSprite;
		} 
		else {
			return republicanRegionSprite;
		}
	}

	public int GetCombatPower(){
		ArmyValues armyValues = FindObjectOfType<ArmyValues> ();
			
		int combatPower = 0;
		foreach(RegionArmySlot armySlot in armySlots){
			if(armySlot.armyType != ArmyType.Empty){
				combatPower += (armySlot.armyAmount * armyValues.GetArmy(armySlot.armyType).attack);
			}
		}

		return combatPower;
	}

	public Region[] GetBorderingRegions (){
		return FindObjectOfType<GameManager>().GetRegionsBorderingRegion (this);
	}

	public bool HasEnemyTroopsInBorderRegions(){
		foreach(Region borderRegion in GetBorderingRegions()){
			if(borderRegion.isNazi != this.isNazi &&
				borderRegion.HasAnyTroops()){
				return true;
			}
		}

		return false;
	}

}
