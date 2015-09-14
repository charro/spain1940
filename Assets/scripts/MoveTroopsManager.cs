using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveTroopsManager : MonoBehaviour {

	public Text toRegionText;
	public Text fromRegionText;

	public GameObject[] unitSlotsFrom;
	public GameObject[] unitSlotsTo;

	private Region fromRegion;
	private Region toRegion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetFromRegion(Region region){
		fromRegion = region;
		fromRegionText.text = fromRegion.name;
	}

	public void SetToRegion(Region region){
		toRegion = region;
		toRegionText.text = toRegion.name;

		// Set the units corresponding to this region
	}

	public void RefreshPanels(){
		// Set the units corresponding to region FROM
		RegionArmySlot[] fromArmySlots = fromRegion.GetArmySlots ();
		
		for(int i=0; i<fromArmySlots.Length && i<unitSlotsFrom.Length; i++){
			if(fromArmySlots[i].armyType == ArmyType.Empty){
				unitSlotsFrom[i].SetActive(false);
			}
			else{
				unitSlotsFrom[i].SetActive(true);
				unitSlotsFrom[i].GetComponentInChildren<Text>().text = "X " + fromArmySlots[i].armyAmount;
				unitSlotsFrom[i].GetComponentInChildren<Image>().sprite = 
					FindObjectOfType<ArmySprites>().armySpritesDictionary[fromArmySlots[i].armyType];
			}
		}

		// Set the units corresponding to region TO
		RegionArmySlot[] toArmySlots = toRegion.GetArmySlots ();
		
		for(int i=0; i<toArmySlots.Length && i<unitSlotsTo.Length; i++){
			if(toArmySlots[i].armyType == ArmyType.Empty){
				unitSlotsTo[i].SetActive(false);
			}
			else{
				unitSlotsTo[i].SetActive(true);
				unitSlotsTo[i].GetComponentInChildren<Text>().text = "X " + toArmySlots[i].armyAmount;
				unitSlotsTo[i].GetComponentInChildren<Image>().sprite = 
					FindObjectOfType<ArmySprites>().armySpritesDictionary[toArmySlots[i].armyType];
			}
		}
	}

	/**
	 * Move a unit from left region to right (if possible)
	 **/
	public void MoveUnitFromSlot(int slot){
		RegionArmySlot fromSlot = fromRegion.GetArmySlots()[slot];

		// Set the units corresponding to region TO
		RegionArmySlot[] toArmySlots = toRegion.GetArmySlots ();

		// Check if there is already a slot with that type of unit and add it there
		foreach(RegionArmySlot toSlot in toArmySlots){
			if(toSlot.armyType == fromSlot.armyType){
				toSlot.armyAmount++;
				fromSlot.armyAmount--;
				RefreshPanels();
				return;
			}

			// Army type not yet found. If this is an empty slot, create it now
			if(toSlot.armyType == ArmyType.Empty){
				toSlot.armyType = fromSlot.armyType;
				toSlot.armyAmount++;
				RefreshPanels();
				return;
			}
		}
	}
}
