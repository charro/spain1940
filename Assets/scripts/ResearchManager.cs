using UnityEngine;
using System.Collections;

public class ResearchManager : MonoBehaviour {

	private Technology currentResearchedTechnology = null;
	private Technology lastSelectedTechnology = null;
	private int turnsToEndResearching = 0;

	// Use this for initialization
	void Start () {
		EventManager.OnPassTurn += PassTurn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Technology GetCurrentResearchedTechnology(){
		return currentResearchedTechnology;
	}

	public void SetSelectedTechnology(Technology selectedTechnology){
		 lastSelectedTechnology = selectedTechnology;
	}

	public void ShowConfirmationPopUp(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmResearch);
	}

	public void StartResearchingTechnology(){
		// Set as current researched technology
		currentResearchedTechnology =  lastSelectedTechnology;
		turnsToEndResearching = currentResearchedTechnology.turnsNeeded;

		// Decrease corresponding action points
		// FindObjectOfType<EconomyManager> ().decreaseActionPoints (currentResearchedTechnology.actionsNeeded);
		// FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
		FindObjectOfType<GameManager>().EndActionAndSwitchToIdleMap(currentResearchedTechnology.actionsNeeded);
		FindObjectOfType<DropDownMessages> ().ShowDropDownMessageForSecs ("RESEARCH OF TECHNOLOGY " + currentResearchedTechnology.name + " STARTED", 5);
	}

	public bool IsAnyTechnologyBeingResearched(){
		return currentResearchedTechnology != null;
	}

	// This function is called automatically by EventManager when Player pass turn
	public void PassTurn(){
		Debug.Log ("RESEARCH MANAGER => PASS TURN.  Turns to finish: " + turnsToEndResearching);

		if(IsAnyTechnologyBeingResearched()){
			if(turnsToEndResearching > 0){
				turnsToEndResearching--;
				if(turnsToEndResearching == 0){
					ResearchFinished();
				}
			}
		}

	}

	private void ResearchFinished(){
		FindObjectOfType<DropDownMessages> ().ShowDropDownMessageForSecs ("TECHNOLOGY " + currentResearchedTechnology.name + " finished", 5);
		currentResearchedTechnology.alreadyResearched = true;
		currentResearchedTechnology = null;
	}

	public bool IsAlreadyResearched(TechnologyType technologyType){
		return FindObjectOfType<TechnologyValues> ().GetTechnology (technologyType).alreadyResearched;
	}

	public int GetRemainingTurnsToEndResearch(){
		return turnsToEndResearching;
	}


	public void RestoreDataFromSaveGame(SaveGameManager.SaveGameData gameData){
		if (gameData.currentResearchedTechnology != TechnologyType.None) {
			currentResearchedTechnology =  FindObjectOfType<TechnologyValues> ().GetTechnology (gameData.currentResearchedTechnology);
			turnsToEndResearching = gameData.turnsToEndResearchingCurrentTech;
		}

		FindObjectOfType<TechnologyValues> ().RestoreDataFromSaveGame (gameData);
	}

	public void FillSaveGameData(SaveGameManager.SaveGameData gameData){
		gameData.currentResearchedTechnology = 
			(currentResearchedTechnology == null ? TechnologyType.None : currentResearchedTechnology.technologyType);

		gameData.turnsToEndResearchingCurrentTech = turnsToEndResearching;

		FindObjectOfType<TechnologyValues> ().FillSaveGameData (gameData);
	}

	public int GetAdditionalAttackForArmy(ArmyType armyType){
		return GetAdditionalAttackForArmyAndDefender (armyType, ArmyType.Empty);
	}


	public int GetAdditionalAttackForArmyAndDefender (ArmyType armyType, ArmyType defenderArmy){
		int additionalAttack = 0;

		// Technologies advantages only apply to our regions, not for Nazi
		if (!Army.isNazi (armyType)) {
			if (IsAlreadyResearched (TechnologyType.HeavyAmmo) && 
				armyType != ArmyType.Milicia) {
				additionalAttack += 2;
			}
		}

		additionalAttack += Army.GetAdditionalAttackForArmyType(armyType, defenderArmy);

		return additionalAttack;
	}

	public int GetAdditionalDefenseForArmyAndRegion(ArmyType armyType, Region defenderArmyRegion){
		return GetAdditionalDefenseForArmyRegionAndAttacker (armyType, defenderArmyRegion, ArmyType.Empty);
	}

	public int GetAdditionalDefenseForArmyRegionAndAttacker (ArmyType armyType, Region defenderArmyRegion, ArmyType attackerType){
		int additionalDefense = 0;

		// Technologies advantages only apply to our regions, not for Nazi
		if(!Army.isNazi(armyType)){
			if(IsAlreadyResearched(TechnologyType.LightArmor)){
				additionalDefense += 1;
			}

			if(IsAlreadyResearched(TechnologyType.HeavyArmor) &&
				armyType != ArmyType.Milicia){
				additionalDefense += 2;
			}

		}

		// Get Additional defense depending on type of armies in combat
		additionalDefense += Army.GetAdditionalDefenseForArmyType (armyType, attackerType);

		// Get Additional defense if highest military building built in army's Region
		if(defenderArmyRegion != null && defenderArmyRegion.GetMilitaryLevel() > 1){
			additionalDefense += 1;
		}

		return additionalDefense;
	}

	public int GetAdditionalSpeedForArmy (ArmyType armyType){
		int additionalSpeed = 0;

		if(IsAlreadyResearched(TechnologyType.JetEngine) && Army.isAirTroop(armyType)){
			additionalSpeed += 2;
		}

		return additionalSpeed;
	}
}
