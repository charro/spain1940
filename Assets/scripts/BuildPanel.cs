using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildPanel : MonoBehaviour {

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
		int actionLevel = selectedRegion.GetActionGenerationLevel ();

		for(int level=0; level<actionLevelButtons.Length && level<actionLevelChecks.Length; level++){
			Button actionLevelButton = actionLevelButtons[level];
			Image actionLevelCheck = actionLevelChecks[level];

			actionLevelCheck.enabled = (actionLevel > level);
			actionLevelButton.interactable = 
				(actionLevel == level && economyManager.haveEnoughActionsPoints(1));
		}
	}

	public void BuildClicked(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmBuild);
	}
}
