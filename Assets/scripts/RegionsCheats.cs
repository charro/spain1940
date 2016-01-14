using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RegionsCheats : MonoBehaviour {

	public RegionType regionSelected;
	public int slotSelected;
	public ArmyType armyTypeOfThiSlot;
	public int unitsOfThisSlot;

	private RegionType lastRegionSelected;
	private int lastSlotSelected;
	private ArmyType lastArmyType;
	private int lastUnitsValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This is called from Unity Editor
	void OnValidate(){
		if(regionSelected != lastRegionSelected){
			OnRegionChanged ();
		}
		else if(slotSelected != lastSlotSelected){
			OnSlotChanged ();
		}
		else if(armyTypeOfThiSlot != lastArmyType){
			OnArmyTypeChanged();
		}
		else if(unitsOfThisSlot != lastUnitsValue){
			OnUnitsValueChanged ();
		}
	}

	private void OnRegionChanged(){
		Region newRegion = FindObjectOfType<GameManager> ().GetRegion (regionSelected);
		lastRegionSelected = regionSelected;

		if (newRegion.HasAnyTroops ()) {
			lastArmyType = armyTypeOfThiSlot = newRegion.GetArmySlots () [slotSelected].armyType;
			lastUnitsValue = unitsOfThisSlot = newRegion.GetArmySlots () [slotSelected].armyAmount;
		} else {
			ResetValuesExceptRegion ();
		}
	}

	private void OnSlotChanged(){
		Region newRegion = FindObjectOfType<GameManager> ().GetRegion (regionSelected);

		if(newRegion.HasAnyTroops()){
			if (slotSelected > newRegion.GetArmySlots ().Length) {
				slotSelected = newRegion.GetArmySlots ().Length - 1;
			}
			lastSlotSelected = slotSelected;

			lastArmyType = armyTypeOfThiSlot = newRegion.GetArmySlots () [slotSelected].armyType;
			lastUnitsValue = unitsOfThisSlot = newRegion.GetArmySlots () [slotSelected].armyAmount;
		}
		else{
			ResetValuesExceptRegion ();
		}

	}

	private void OnArmyTypeChanged(){
		lastArmyType = armyTypeOfThiSlot;

		Region newRegion = FindObjectOfType<GameManager> ().GetRegion (regionSelected);
		newRegion.GetArmySlots () [slotSelected].armyType = armyTypeOfThiSlot;
	}

	private void OnUnitsValueChanged(){
		lastUnitsValue = unitsOfThisSlot;

		// Persist the values
		Region newRegion = FindObjectOfType<GameManager> ().GetRegion (regionSelected);
		newRegion.GetArmySlots () [slotSelected].armyAmount = unitsOfThisSlot;
	}

	private void ResetValuesExceptRegion(){
		lastSlotSelected = slotSelected = 0;
		lastArmyType = armyTypeOfThiSlot = ArmyType.Empty;
		lastUnitsValue = unitsOfThisSlot = 0;
	}
}
