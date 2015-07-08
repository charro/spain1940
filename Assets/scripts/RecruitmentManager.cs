using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecruitmentManager : MonoBehaviour {

	public Dictionary<ArmyType, int> unitPrices;

	public Text recruitmentPointsText;
	public RectTransform recruitmentPanel;

	public GameObject unitGroupPrefab;

	private ArrayList recruitedUnitGroups = new ArrayList();

	private Vector3 firstUnitPosition;

	// Use this for initialization
	void Start () {
		ResetUnits ();
		SetUnitPrices ();
	}
	
	// Update is called once per frame
	void Update () {
		recruitmentPointsText.text = "" + EconomyManager.getRecruitmentPoints();
	}

	public void AddToUnit(int unitTypeInt){
		ArmyType unitType = (ArmyType) unitTypeInt;

		GameObject selectedUnitGroupGameObject = null;

		// Find the unit by type
		foreach(GameObject unitGroupGameObject in recruitedUnitGroups){
			RecruitedUnitGroup recruitedUnitGroup = 
				unitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;

			if(recruitedUnitGroup.UnitType == unitType){
				selectedUnitGroupGameObject = unitGroupGameObject;
				break;
			}
		}

		// Not found, create a new group of this unit type
		if(selectedUnitGroupGameObject == null){
			GameObject instance = Instantiate (unitGroupPrefab) as GameObject;
			instance.transform.SetParent (recruitmentPanel.transform, false);

			// Save the first unit position for further use (will be the position 0,0 of parent Panel)
			if(recruitedUnitGroups.Count == 0){
				firstUnitPosition = instance.transform.position;
			}
			
			RecruitedUnitGroup recruitedUnitGroup = instance.GetComponent<RecruitedUnitGroup>();
			recruitedUnitGroup.UnitType = unitType;
			recruitedUnitGroups.Add(instance);

			selectedUnitGroupGameObject = instance;

			PlaceRecruitedUnitsInContainer();
		}

		RecruitedUnitGroup selectedUnitGroup = 
			selectedUnitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;
		selectedUnitGroup.AddUnit ();

		EconomyManager.decreaseRecruitmentPoints (unitPrices[unitType]);
	}

	public void OnRemoveUnit(RecruitedUnitGroup unit){
		EconomyManager.addRecruitmentPoints(unitPrices[unit.UnitType]);

		if(unit.UnitAmount <= 0){
			RemoveEmptyRecruitedGroups ();
		}
	}

	public void ResetUnits(){
		foreach (GameObject unitGroupGameObject in recruitedUnitGroups) {
			Destroy(unitGroupGameObject);
		}

		recruitedUnitGroups = new ArrayList ();
	}

	// Place the units to their proper place inside the container
	private void PlaceRecruitedUnitsInContainer(){
		Canvas canvas = recruitmentPanel.GetComponentInParent<Canvas>();
		float containerPanelWidth = recruitmentPanel.rect.width;
		float separation = containerPanelWidth / 4;

		for(int i=0; i<recruitedUnitGroups.Count; i++){
			GameObject unitGroup = recruitedUnitGroups[i] as GameObject;
			// RectTransform rectTransform = unitGroup.GetComponent<RectTransform>();

			unitGroup.transform.position = 
				new Vector3(firstUnitPosition.x + (separation * i * canvas.scaleFactor), 
				            firstUnitPosition.y,
				            firstUnitPosition.z);
			/*
			 Vector3 newRelativePosition = 
				new Vector3(rectTransform.localPosition.x + (70*i), rectTransform.localPosition.y, rectTransform.localPosition.z);
			rectTransform.localPosition = newRelativePosition; */
		}
	}

	// Remove any existing empty group
	private void RemoveEmptyRecruitedGroups(){
		ArrayList listToRemove = new ArrayList ();

		foreach(GameObject unitGroup in recruitedUnitGroups){
			RecruitedUnitGroup recruitedUnitGroup = unitGroup.GetComponent<RecruitedUnitGroup>();
			if(!recruitedUnitGroup || recruitedUnitGroup.UnitAmount < 1){
				listToRemove.Add(unitGroup);
			}
		}

		foreach(GameObject unitToRemove in listToRemove){
			recruitedUnitGroups.Remove(unitToRemove);
			Destroy(unitToRemove);
		}
		PlaceRecruitedUnitsInContainer ();
	}

	/**
	 * Perform the recruitment, making the changes permanent **/
	public void PerformRecruitment(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		Region selectedRegion = gameManager.GetSelectedRegion ();

		foreach(GameObject unitGroupGameObject in recruitedUnitGroups){
			RecruitedUnitGroup unitGroup = 
				unitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;

			selectedRegion.AddUnitsToArmy(unitGroup.UnitType, unitGroup.UnitAmount);
		}
	}

	/**
	 * Rollback all changes and come back */
	public void CancelRecruitment(){

	}

	private void SetUnitPrices(){
		unitPrices = new Dictionary<ArmyType, int>();

		unitPrices.Add (ArmyType.Soldier, 15);
		unitPrices.Add (ArmyType.TankToro, 25);
		unitPrices.Add (ArmyType.TankBisonte, 45);
	}

}
