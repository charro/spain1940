using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public const int BUILDING_TYPE_CLICKED_NONE = -1;
	public const int BUILDING_TYPE_CLICKED_ACTION = 0;
	public const int BUILDING_TYPE_CLICKED_MILITARY = 1;

	public BuildPanel buildPanel;

	private int typeOfBuildingClicked = BUILDING_TYPE_CLICKED_NONE;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ConfirmBuilding(){
		switch(typeOfBuildingClicked){
			case BUILDING_TYPE_CLICKED_ACTION:
				IncreaseActionLevel ();
				break;
			case BUILDING_TYPE_CLICKED_MILITARY:
				IncreaseMilitaryLevel ();
				break;
			default:
				Debug.LogError ("ERROR: ConfirmBuilding called, but no type of building selected");
				break;
		}
	}

	public void ActionBuildingClicked(){
		typeOfBuildingClicked = BUILDING_TYPE_CLICKED_ACTION;
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmBuild);
	}

	public void MilitaryBuildingClicked(){
		typeOfBuildingClicked = BUILDING_TYPE_CLICKED_MILITARY;
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmBuild);
	}

	private void IncreaseMilitaryLevel(){
		Region selectedRegion = FindObjectOfType<GameManager> ().GetSelectedRegion ();
		selectedRegion.IncreaseMilitaryLevel ();

		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		economyManager.decreaseActionPoints (1);
		economyManager.RecalculateTotalMilitaryGenerationPoints ();
		buildPanel.RefreshElements();

		typeOfBuildingClicked = BUILDING_TYPE_CLICKED_NONE;
	}

	private void IncreaseActionLevel(){
		Region selectedRegion = FindObjectOfType<GameManager> ().GetSelectedRegion ();
		selectedRegion.IncreaseActionGenerationLevel ();

		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		economyManager.decreaseActionPoints (1);
		economyManager.recalculateMaximumActionsPerTurn ();
		buildPanel.RefreshElements();

		typeOfBuildingClicked = BUILDING_TYPE_CLICKED_NONE;
	}
}
