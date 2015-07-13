using UnityEngine;
using System.Collections;

/**
 * All logic regarding the status of the game at some point should be here.
 * Also, what to do next, when, for instance, a region is touched
 * */
public class GameStateMachine : MonoBehaviour {

	private GameState previousState;
	private GameState currentState;
	private GameManager gameManager;
	private Messages messages;

	// Use this for initialization
	void Start () {
		currentState = GameState.IdleMapState;
		gameManager = FindObjectOfType<GameManager> ();
		messages = FindObjectOfType<Messages> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameState GetCurrentState(){
		return currentState;
	}

	public void ChangeToState(int state){
		ChangeToState ((GameState) state);
	}

	public void ChangeToState(GameState newState){
		previousState = currentState;
		currentState = newState;

		InitState (currentState);
	}

	private void InitState(GameState state){
		switch(state){
			case GameState.ActionsGUIState:
				break;
			case GameState.MoveTroopsState:
				EnterMoveTroopsState();
				break;
		}
	}

	void EnterActionsGUIState(){

	}

	void EnterMoveTroopsState(){
		Region fromRegion = gameManager.GetSelectedRegion();

		// Hide GUI Panels, and show region Map with all regions disabled
		gameManager.ShowMapAllRegionsDisabled ();

		// Enable only neighbourg regions that are no Nazi
		Region[] neighbourRegions = gameManager.GetRegionsBorderingRegion (fromRegion);
		foreach(Region region in neighbourRegions){
			if(!region.isNazi){
				region.Enable();
			}
		}

		// Show the corresponding messages
		messages.showWhereToMoveText ();
	}

	public void OnRegionTouched(Region region){
		switch(currentState){
			case GameState.IdleMapState:
				Debug.Log("Region touched while we are on IdleMapState State");
				region.toggleSelected();
				break;
			case GameState.ActionsGUIState:
				Debug.Log("Region touched while we are on ActionsGUI State");
				break;
			case GameState.MoveTroopsState:
				Debug.Log("Region touched while we are on MoveTroopsState. Region selected to move troops to: " + region.name);
				UIManager.ShowMoveTroopsPanel();
				break;
		}
	}
}
