﻿using UnityEngine;
using System.Collections;

/**
 * All logic regarding the status of the game at some point should be here.
 * Also, what to do next, when, for instance, a region is touched
 * */
public class GameStateMachine : MonoBehaviour {

	private GameState previousState;
	private GameState currentState;

	// Use this for initialization
	void Start () {
		currentState = GameState.IdleMapState;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameState GetCurrentState(){
		return currentState;
	}
		
	public void SwitchToState(int state){
		SwitchToState ((GameState) state);
	}

	public void SwitchToState(GameState newState){
		previousState = currentState;
		currentState = newState;

		InitState (currentState);
	}

	private void InitState(GameState state){
		switch(state){
			case GameState.IdleMapState:
				EnterIdleMapState();
				break;
			case GameState.ActionsGUIState:
				break;
			case GameState.MoveTroopsState:
				EnterMoveTroopsState();
				break;
			case GameState.NewSpyState:
				EnterNewSpyState();
				break;
		case GameState.AttackState:
				EnterAttackState();
				break;
		}
	}

	void EnterIdleMapState(){
		FindObjectOfType<GameManager>().ShowMapAndHUD ();
	}

	void EnterActionsGUIState(){

	}

	void EnterMoveTroopsState(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		Region fromRegion = gameManager.GetSelectedRegion();

		UIManager.hideAllPanels ();
		gameManager.ShowMapAllRegionsDisabled ();


		// Enable only neighbourg regions that are no Nazi
		Region[] neighbourRegions = gameManager.GetRegionsBorderingRegion (fromRegion);
		foreach(Region region in neighbourRegions){
			if(!region.isNazi){
				region.Enable();
			}
		}
			
		// Set where to move the troops from
		FindObjectOfType<MoveTroopsManager> ().SetFromRegion (fromRegion);

		// Show the corresponding messages
		UIManager.GetOnMapMessagesPanel().showWhereToMoveText ();
	}

	void EnterNewSpyState(){
		FindObjectOfType<GameManager>().ShowMapEnemyNonSpiedRegionsOnly ();
		// Show the corresponding messages
		UIManager.GetOnMapMessagesPanel().showWhereToSpyText ();
	}

	void EnterAttackState(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		FindObjectOfType<CombatManager> ().SetDefenderRegion(gameManager.GetSelectedRegion());
		gameManager.ShowMapPossibleAttackersOfCurrentSelectedRegionOnly();
		// Show the corresponding messages
		UIManager.GetOnMapMessagesPanel().showWhereToAttackText();
	}

	public void OnRegionTouched(Region region){
		switch(currentState){
			case GameState.IdleMapState:
			Debug.Log("Region touched while we are on IdleMapState State. Toggle selected to: " + !region.isSelected());
				region.toggleSelected();
				break;
			case GameState.ActionsGUIState:
				Debug.Log("Region touched while we are on ActionsGUI State");
				break;
			case GameState.MoveTroopsState:
				Debug.Log("Region touched while we are on MoveTroopsState. Region selected to move troops to: " + region.name);
				// Set where to move the troops to
				FindObjectOfType<MoveTroopsManager>().SetToRegion (region);
				UIManager.hideAllPanels();
				UIManager.ShowMoveTroopsPanel();
				FindObjectOfType<MoveTroopsManager>().RefreshPanels();
				break;
		case GameState.NewSpyState:
			Debug.Log("Region touched while we are on NewSpyState. Region selected to send spy to: " + region.name);
			// Confirm where to send the spy
			FindObjectOfType<SpyManager>().ConfirmAddNewSpyToRegion(region);
			break;
		case GameState.AttackState:
			Debug.Log("Region touched while we are on AttackState. Region selected to send attack from: " + region.name);
			// Confirm where to send attack from
			FindObjectOfType<CombatManager>().ShowConfirmStartAttackFrom(region);
			break;
		}
	}

	public void SwitchToStateIdle(){
		SwitchToState (GameState.IdleMapState);
	}
}
