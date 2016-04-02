using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIManager : MonoBehaviour {

	public int baseRecruitPointsPerRegion;
	public float recruitPointsMultiplierPerTurn;
	public int turnsNeededForNextResearch;

	private List<Region> naziRegions = new List<Region>();
	private int turnNumber;

	private int accumulatedRecruitmentPoints = 0;

	private List<ArmyType> researchedArmyTypes = new List<ArmyType> ();
	private int turnOfLastResearchedArmy = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoAITurnActions(){
		// Gather all info needed to take this turn decisions
		RefreshNeededDataForThisTurn ();

		DoResearchLogic ();

		DoRecruitmentLogic ();

		DoAttackLogic ();
	}

	//******************************************  RECRUITMENT *********************************************/
	private void DoRecruitmentLogic(){
		// First, check how many regions we have as Nazi
		int numberOfNaziRegions = naziRegions.Count;
		int recruitPointsForThisTurn =  Mathf.FloorToInt(numberOfNaziRegions * baseRecruitPointsPerRegion * 
			turnNumber * (1.0f + recruitPointsMultiplierPerTurn));

		Debug.Log ("IA: Recruit points generated for this turn = " + recruitPointsForThisTurn);

		// Decide if a new unit type has been researched


		// Decide if perform recruitment or save points for later
		if(DecideIfPerformRecruitment()){
			PerformRecruitment ();
		}
		else{
			accumulatedRecruitmentPoints += recruitPointsForThisTurn;
		}
	}

	private bool DecideIfPerformRecruitment(){
		accumulatedRecruitmentPoints = 0;
		return true;
	}

	private void PerformRecruitment (){
	}

	//********************************************  RESEARCH *********************************************/
	private void DoResearchLogic(){

		// Loop trough all army types till finding the first non-developed one
		foreach(Army army in FindObjectOfType<ArmyValues>().GetNaziArmies()){
			if(!IsArmyTypeAlreadyResearched(army.armyType)){
				// Increase the chance of having a new unit type depending on the turns passed since last research
				if(turnNumber - turnOfLastResearchedArmy > turnsNeededForNextResearch){
					turnOfLastResearchedArmy = turnNumber;
					Debug.Log ("Nazi AI: Nazis just discovered army " + army.armyDescription);
					researchedArmyTypes.Add (army.armyType);
				}
				return;
			}
		}
	}

	private bool IsArmyTypeAlreadyResearched(ArmyType type){
		foreach(ArmyType researchedArmy in researchedArmyTypes){
			if(type == researchedArmy){
				return true;
			}
		}

		return false;
	}

	//******************************************** ATTACK *************************************************/
	private void DoAttackLogic(){

	}

	private void RefreshNeededDataForThisTurn(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		naziRegions = gameManager.GetAllNaziRegions ();
		turnNumber = gameManager.GetCurrentTurnNumber ();
	}
	
}
