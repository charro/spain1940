using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUp : MonoBehaviour {

	public Text titleText;
	public Text bodyText;
	private PopUpType type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewPopUp(PopUpType popUpType){
		PrePopUpActions ();

		type = popUpType;

		switch(type){
			case PopUpType.ConfirmPassTurn:
				titleText.text = "Confirm Pass Turn";
				bodyText.text = "You still have some action points. Do you really want to pass turn ?";	
				break;
			case PopUpType.ConfirmRecruitment:
				titleText.text = "Confirm Recruitment";
				bodyText.text = "To make this recruitment will consum 1 Action Point. Do you confirm the recruitment ?";	
				break;
			case PopUpType.ConfirmBuild:
				titleText.text = "Confirm Build";
				bodyText.text = "To make this building will consum 1 Action Point. Do you confirm the build ?";	
				break;
			case PopUpType.ConfirmNewSpy:
				titleText.text = "Confirm Spy Region";
				bodyText.text = "Send a spy to this region will consum 1 Action Point. Do you confirm to send the spy ?";	
				break;
			case PopUpType.ConfirmResearch:
				titleText.text = "Confirm New Research";
				bodyText.text = "Start this research will consum 1 Action Point. Do you confirm to start this research ?";	
				break;
			case PopUpType.ConfirmAttack:
				CombatManager combatManager = FindObjectOfType<CombatManager>();
				titleText.text = "Confirm Attack " + combatManager.GetDefenderRegion() + " from " + combatManager.GetAttackerRegion();
				bodyText.text = "Start this attack will consum 1 Action Point. Do you confirm to start the attack ?";	
				break;
			case PopUpType.MoveTroops:
				titleText.text = "Confirm Order";
				bodyText.text = "Move these troops will consum 1 Action Point. Do you confirm to do the move ?";	
				break;
			case PopUpType.RestartGame:
				titleText.text = "Surrender to Nazi army";
				bodyText.text = "This will finish and restart your game. Do you confirm to surrender cowardly to the Nazis ?";	
				break;
		}
	}

	public void YesClicked(){
		PostPopUpActionsYesClicked ();

		switch(type){
			case PopUpType.ConfirmPassTurn:
				FindObjectOfType<GameManager> ().EndCurrentTurn();
				break;
			case PopUpType.ConfirmRecruitment:
				FindObjectOfType<RecruitmentManager>().PerformRecruitment();
				break;
			case PopUpType.ConfirmBuild:
				FindObjectOfType<BuildManager>().ConfirmBuilding();
				break;
		case PopUpType.ConfirmNewSpy:
				FindObjectOfType<SpyManager>().AddNewSpy();
				break;
		case PopUpType.ConfirmResearch:
				FindObjectOfType<ResearchManager>().StartResearchingTechnology();
				break;
		case PopUpType.ConfirmAttack:
				FindObjectOfType<CombatManager>().StartCombat(true);
				break;
		case PopUpType.MoveTroops:
				FindObjectOfType<MoveTroopsManager> ().EndUnitsMove ();
				break;
		case PopUpType.RestartGame:
				FindObjectOfType<LevelManager> ().RestartGame ();
				break;
		}

	}

	public void NoClicked(){

		switch(type){
			case PopUpType.ConfirmPassTurn:
			case PopUpType.ConfirmRecruitment:
			case PopUpType.ConfirmBuild:
			case PopUpType.ConfirmResearch:
			case PopUpType.MoveTroops:
			case PopUpType.RestartGame:
				break;
		case PopUpType.ConfirmNewSpy:
				FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
				break;
		}

		PostPopUpActionsNoClicked ();
	}

	private void PrePopUpActions(){
		//Disable All Other elements on screen
		FindObjectOfType<GameStateMachine>().ChangeToState(GameState.PopUpShownState);
	}


	private void PostPopUpActionsYesClicked(){
		FindObjectOfType<UIManager>().HidePopUp();
		// Enable again all other elements
		FindObjectOfType<GameStateMachine>().ChangeToState(GameState.IdleMapState);
	}

	private void PostPopUpActionsNoClicked(){
		FindObjectOfType<UIManager>().HidePopUp();
		// Enable again all other elements
		FindObjectOfType<GameStateMachine>().ChangeBackToPreviousState();
	}
}
