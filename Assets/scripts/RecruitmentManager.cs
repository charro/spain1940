using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecruitmentManager : MonoBehaviour {

	public Text recruitmentPointsText;
	public RectTransform recruitmentPanel;

	public GameObject unitGroupPrefab;

	private ArrayList recruitedUnitGroupList = new ArrayList();

	private Vector3 firstUnitPosition;

	private EconomyManager economyManager;

	private int spentRecruitmentPoints;

	// Use this for initialization
	void Start () {
		economyManager = FindObjectOfType<EconomyManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void RestartUI(){
		ResetUnits ();
		UpdateRecruitmentPointsText ();
	}

	public void AddToUnit(int unitTypeInt){
		AddToUnit ((ArmyType) unitTypeInt);
	}

	public void AddToUnit(ArmyType unitType){

		// First check if there are enough resources to perform Recruit
		if(!economyManager.haveEnoughRecruitmentPoints(FindObjectOfType<ArmyValues>().armyPricesDictionary[unitType])){
			Animator textAnimation = recruitmentPointsText.GetComponent<Animator>();
			textAnimation.SetTrigger("trigger");
			return;
		}

		GameObject selectedUnitGroupGameObject = null;

		// Find the unit by type
		foreach(GameObject unitGroupGameObject in recruitedUnitGroupList){
			RecruitedUnitGroup recruitedUnitGroup = 
				unitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;

			if(recruitedUnitGroup.UnitType == unitType){
				selectedUnitGroupGameObject = unitGroupGameObject;
				break;
			}
		}

		// Not found, create a new group of this unit type
		if(selectedUnitGroupGameObject == null){
			GameObject newUnitInstance = Instantiate (unitGroupPrefab) as GameObject;
			newUnitInstance.transform.SetParent (recruitmentPanel.transform, false);

			// Save the first unit position for further use (it will be the position 0,0 of parent Panel)
			if(recruitedUnitGroupList.Count == 0){
				firstUnitPosition = newUnitInstance.transform.position;
			}
			
			RecruitedUnitGroup recruitedUnitGroup = newUnitInstance.GetComponent<RecruitedUnitGroup>();
			recruitedUnitGroup.UnitType = unitType;
			recruitedUnitGroup.unitImage.sprite = FindObjectOfType<ArmyValues>().armySpritesDictionary[unitType];

			recruitedUnitGroupList.Add(newUnitInstance);

			selectedUnitGroupGameObject = newUnitInstance;

			PlaceRecruitedUnitsInContainer();
		}

		RecruitedUnitGroup selectedUnitGroup = 
			selectedUnitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;
		selectedUnitGroup.AddUnit ();

		int recruitmentPointsNeeded = FindObjectOfType<ArmyValues> ().armyPricesDictionary [unitType];
		economyManager.decreaseRecruitmentPoints (recruitmentPointsNeeded);
		spentRecruitmentPoints += recruitmentPointsNeeded;
		// Update UI Recruitment Points Text
		UpdateRecruitmentPointsText ();
	}

	public void OnRemoveUnit(RecruitedUnitGroup unit){
		int unitPrice = FindObjectOfType<ArmyValues> ().armyPricesDictionary [unit.UnitType];
		economyManager.addRecruitmentPoints(unitPrice);
		spentRecruitmentPoints -= unitPrice;
		UpdateRecruitmentPointsText ();

		if(unit.UnitAmount <= 0){
			RemoveEmptyRecruitedGroups ();
		}
	}

	public void ResetUnits(){
		foreach (GameObject unitGroupGameObject in recruitedUnitGroupList) {
			Destroy(unitGroupGameObject);
		}

		recruitedUnitGroupList = new ArrayList ();
		spentRecruitmentPoints = 0;
	}

	// Place the units to their proper place inside the container
	private void PlaceRecruitedUnitsInContainer(){
		Canvas canvas = recruitmentPanel.GetComponentInParent<Canvas>();
		float containerPanelWidth = recruitmentPanel.rect.width;
		float separation = containerPanelWidth / 4;

		for(int i=0; i<recruitedUnitGroupList.Count; i++){
			GameObject unitGroup = recruitedUnitGroupList[i] as GameObject;
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

		foreach(GameObject unitGroup in recruitedUnitGroupList){
			RecruitedUnitGroup recruitedUnitGroup = unitGroup.GetComponent<RecruitedUnitGroup>();
			if(!recruitedUnitGroup || recruitedUnitGroup.UnitAmount < 1){
				listToRemove.Add(unitGroup);
			}
		}

		foreach(GameObject unitToRemove in listToRemove){
			recruitedUnitGroupList.Remove(unitToRemove);
			Destroy(unitToRemove);
		}
		PlaceRecruitedUnitsInContainer ();
	}

	public void CheckBeforePerformingRecruitment(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmRecruitment);
	}

	/**
	 * Perform the recruitment, making the changes permanent **/
	public void PerformRecruitment(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		Region selectedRegion = gameManager.GetSelectedRegion ();

		foreach(GameObject unitGroupGameObject in recruitedUnitGroupList){
			RecruitedUnitGroup unitGroup = 
				unitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;

			selectedRegion.AddUnitsToArmy(unitGroup.UnitType, unitGroup.UnitAmount);
		}

		gameManager.EndActionAndShowMap ();
	}

	/**
	 * Rollback all changes and come back */
	public void CancelRecruitment(){
		FindObjectOfType<GameManager> ().ShowMapAndHUD();
		// Restore the spent recruitment points
		economyManager.addRecruitmentPoints (spentRecruitmentPoints);
	}

	private void UpdateRecruitmentPointsText(){
		recruitmentPointsText.text = "" + economyManager.getRecruitmentPoints();
	}
}
