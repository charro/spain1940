using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () {
		int maxSlots = GetMaxSlots ();
		armySlots = new RegionArmySlot[maxSlots];
		ClearArmySlots ();
		spriteRenderer = GetComponent<SpriteRenderer>();
		FindObjectOfType<GameManager> ().AddRegionToList(this);

		// FIXME: This is only for Testing. Add units to Enemy Regions
		if(isNazi){
			AddUnitsToArmy (ArmyType.Milicia, 60);
			AddUnitsToArmy (ArmyType.TankBisonte, 5);
		}
		if(regionType == RegionType.Asturias){
			AddUnitsToArmy (ArmyType.TankLince, 20);
			AddUnitsToArmy (ArmyType.TankToro, 7);
		}
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
		
		if(isNazi){
			spriteRenderer.sprite = naziRegionSprite;
		}
		else{
			spriteRenderer.sprite = republicanRegionSprite;
		}
	}

	public void AddUnitsToArmy(ArmyType type, int units){

		// Check if the type is being used already. 
		// Otherwise just add units 
		for(int i=0; i<armySlots.Length; i++){

			if(armySlots[i].armyType == ArmyType.Empty){
				armySlots[i].armyType = (ArmyType)type;
				armySlots[i].armyAmount = units;
				break;
			}
			else if(armySlots[i].armyType == type){
				armySlots[i].armyAmount += units;
			}
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
		spriteRenderer.material = UIManager.GetDefaultMaterial();
	}

	public void Disable(){
		enabledRegion = false;
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
}
