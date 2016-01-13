using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildPanel : MonoBehaviour {

	public Text thisRegionMilitaryPoints;
	public Text currentTotalActionPoints;
	public Text nextLevelActionPoints;

	// Actions Buildings
	public Text[] actionLevelPointsTexts;
	public Button[] actionLevelButtons;
	public Image[] actionLevelChecks;

	// Military Buildings
	public Text[] militaryPointsTexts;
	public Button[] militaryButtons;
	public Image[] militaryChecks;


	void Awake(){
		RefreshElements ();
	}

	void OnEnable(){
		RefreshElements();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshElements(){
		Region selectedRegion = FindObjectOfType<GameManager> ().GetSelectedRegion ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		BuildValues buildValues = FindObjectOfType<BuildValues> ();

		// Refresh action buildings info 
		int actionLevel = selectedRegion.GetActionGenerationLevel ();

		for(int level=0; level<actionLevelButtons.Length && level<actionLevelChecks.Length &&
			level<actionLevelPointsTexts.Length; level++){
			Button actionLevelButton = actionLevelButtons[level];
			Image actionLevelCheck = actionLevelChecks[level];
			Text actionLevelPointsText = actionLevelPointsTexts[level];

			bool isResearched = buildValues.IsBuildingResearched (false, level);
			actionLevelCheck.enabled = (actionLevel > level);
			actionLevelButton.interactable = 
				(isResearched && actionLevel == level && economyManager.haveEnoughActionsPoints(1));
			actionLevelPointsText.text  = "+" + buildValues.actionGenerationPointsPerBuilding[level];
		}

		// Refresh military buildings info 
		int militaryLevel = selectedRegion.GetMilitaryLevel ();

		for(int level=0; level<militaryButtons.Length && level<militaryChecks.Length &&
			level<militaryPointsTexts.Length; level++){
			Button militaryLevelButton = militaryButtons[level];
			Image militaryCheck = militaryChecks[level];
			Text militaryPointsText = militaryPointsTexts[level];

			bool isResearched = buildValues.IsBuildingResearched (true, level);
			militaryCheck.enabled = (militaryLevel > level);
			militaryLevelButton.interactable = 
				(isResearched && militaryLevel == level && economyManager.haveEnoughActionsPoints(1));
			militaryPointsText.text  = "+" + buildValues.militaryPointsPerBuilding[level];
		}

		thisRegionMilitaryPoints.text = "" + selectedRegion.GetMilitaryPointsGeneration ();
		currentTotalActionPoints.text = "" + economyManager.GetTotalActionGenerationPoints ();
		nextLevelActionPoints.text = "" + economyManager.GetActionPointsThresholdForNextLevel ();
	}

}