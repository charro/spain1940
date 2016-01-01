using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildPanel : MonoBehaviour {

	public Text currentTotalActionPoints;
	public Text nextLevelActionPoints;
	public Text[] actionLevelPointsTexts;
	public Button[] actionLevelButtons;
	public Image[] actionLevelChecks;

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

		int actionLevel = selectedRegion.GetActionGenerationLevel ();

		for(int level=0; level<actionLevelButtons.Length && level<actionLevelChecks.Length; level++){
			Button actionLevelButton = actionLevelButtons[level];
			Image actionLevelCheck = actionLevelChecks[level];
			Text actionLevelPointsText = actionLevelPointsTexts[level];

			actionLevelCheck.enabled = (actionLevel > level);
			actionLevelButton.interactable = 
				(actionLevel == level && economyManager.haveEnoughActionsPoints(1));
			actionLevelPointsText.text  = "+" + buildValues.actionGenerationPointsPerBuilding[level];
		}

		currentTotalActionPoints.text = "Current Action Generation Points: " + economyManager.GetTotalActionGenerationPoints ();
		nextLevelActionPoints.text = "Points for Next Action Generation Level: " + economyManager.GetActionPointsThresholdForNextLevel ();
	}

	public void BuildClicked(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmBuild);
	}
}