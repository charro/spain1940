using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {

	public bool selected;
	public bool isNazi;
	public Sprite spanishRegionSprite;
	public Sprite naziRegionSprite;
	public bool enabledRegion;
	public RegionType regionType;

	private SpriteRenderer spriteRenderer;

	private ArmyType[] armySlotsTypes;
	public ArmyType[] ArmySlotsTypes(){
		return armySlotsTypes;
	}

	private int[] armySlotsUnits;
	public int[] ArmySlotsUnits(){
		return armySlotsUnits;
	}

	// Use this for initialization
	void Start () {
		int maxSlots = GetMaxSlots ();
		armySlotsTypes = new ArmyType[maxSlots];
		armySlotsUnits = new int[maxSlots];
		spriteRenderer = GetComponent<SpriteRenderer>();

		FindObjectOfType<GameManager> ().AddRegionToList(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int GetMaxSlots(){
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

	public void SetNaziConquered(bool naziConquered){
		isNazi = naziConquered;
		
		if(isNazi){
			spriteRenderer.sprite = naziRegionSprite;
		}
		else{
			spriteRenderer.sprite = spanishRegionSprite;
		}
	}

	public void AddUnitsToArmy(ArmyType type, int units){

		// Check if the type is being used already. 
		// Otherwise just add units 
		for(int i=0; i<armySlotsTypes.Length; i++){
			if(armySlotsTypes[i] == ArmyType.Empty){
				armySlotsTypes[i] = (ArmyType)type;
				armySlotsUnits[i] = units;
				break;
			}
			else if(armySlotsTypes[i] == type){
				armySlotsUnits[i] += units;
			}
		}
	}

	public void ClearArmySlots(){
		for(int i=0; i<armySlotsTypes.Length; i++){
			armySlotsTypes[i] = ArmyType.Empty;
		}

		for(int i=0; i<armySlotsUnits.Length; i++){
			armySlotsUnits[i] = 0;
		}
	}

	public bool IsThereAnEmptySlot(){
		foreach(ArmyType type in armySlotsTypes){
			if(type == ArmyType.Empty){
				return true;	
			}
		}

		return false;
	}

	public void Enable(){
		enabledRegion = true;
		spriteRenderer.material = UIManager.GetDefaultMaterial();
	}

	public void Disable(){
		enabledRegion = false;
		spriteRenderer.material = UIManager.GetDisabledMaterial();
	}
}
