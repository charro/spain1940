using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MoveTroopsManager : MonoBehaviour {

	public Text toRegionText;
	public Text fromRegionText;

	public GameObject[] unitSlotsFrom;
	public GameObject[] unitSlotsTo;

	private Region fromRegion;
	private Region toRegion;

	private RegionArmySlot[] startingUnitsOfRegionFrom;
	private RegionArmySlot[] startingUnitsOfRegionTo;

	private int unitsPerClick;

	public void SetUnitPerClick(int units){
		unitsPerClick = units;
	}

	// Use this for initialization
	void Start () {
		int maxSlots = (FindObjectOfType<ArmyManager> ()).MAX_ARMY_SLOTS_PER_REGION;
		startingUnitsOfRegionFrom = new RegionArmySlot[maxSlots];
		startingUnitsOfRegionTo = new RegionArmySlot[maxSlots];
		unitsPerClick = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetFromRegion(Region region){
		fromRegion = region;
		fromRegionText.text = fromRegion.name;
		CopyRegionArray (fromRegion.GetArmySlots (), startingUnitsOfRegionFrom);
	}

	public void SetToRegion(Region region){
		toRegion = region;
		toRegionText.text = toRegion.name;
		CopyRegionArray (toRegion.GetArmySlots (), startingUnitsOfRegionTo);
	}

	private void CopyRegionArray(RegionArmySlot[] sourceArray, RegionArmySlot[] toArray){
		for(int i=0; i<sourceArray.Length; i++){
			RegionArmySlot copiedArmySlot = new RegionArmySlot();
			copiedArmySlot.armyType = sourceArray[i].armyType; 
			copiedArmySlot.armyAmount = sourceArray[i].armyAmount;
			toArray[i] = copiedArmySlot;
		}
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
					FindObjectOfType<ArmyValues>().GetArmy(fromArmySlots[i].armyType).sprite;
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
					FindObjectOfType<ArmyValues>().GetArmy(toArmySlots[i].armyType).sprite;
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
			if (fromSlot.armyAmount >= unitsPerClick) {

				if (toSlot.armyType == fromSlot.armyType) {
					Debug.Log ("MoveTroopsManager: Move unit of type " + toSlot.armyType + " from region " +
					fromRegion.name + " to region " + toRegion.name);
					toSlot.addUnits (unitsPerClick);
					fromSlot.removeUnits (unitsPerClick);
					RefreshPanels ();
					return;
				}

				// Army type not yet found. If this is an empty slot, create it now
				if (toSlot.armyType == ArmyType.Empty) {
					toSlot.armyType = fromSlot.armyType;
					toSlot.addUnits (unitsPerClick);
					fromSlot.removeUnits (unitsPerClick);
					RefreshPanels ();
					return;
				}

			}
		}
	}

	public void CancelUnitsMove(){
		CopyRegionArray (startingUnitsOfRegionFrom, fromRegion.GetArmySlots ());
		CopyRegionArray (startingUnitsOfRegionTo, toRegion.GetArmySlots ());
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
	}

	public void EndUnitsMove(){
		fromRegion.SortTroopSlots ();
		toRegion.SortTroopSlots ();
		FindObjectOfType<GameManager>().EndActionAndSwitchToIdleMap ();
	}
}
