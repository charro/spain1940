using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIManager : MonoBehaviour {

	public bool testMode;

	public int baseRecruitPointsPerRegion;
	public float recruitPointsMultiplierPerTurn;
	public int turnsNeededForNextResearch;			// Maximum turns to make a new research
	public int turnsNeededForNextSpy;				// Maximum turns to have a successful spying

	private List<Region> naziRegions = new List<Region>();
	private int turnNumber;

	private int accumulatedRecruitmentPoints = 0;

	private List<Army> researchedArmies = new List<Army> ();
	private int turnOfLastResearchedArmy = 0;
	private int turnOfLastSuccessfulSpy = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Will return true in case the AI decisions lead to a combat
	 **/
	public bool DoAITurnActions(){
		// Gather all info needed to take this turn decisions
		RefreshNeededDataForThisTurn ();

		DoResearchLogic ();

		DoRecruitmentLogic ();

		return DoAttackLogic (DoSpyLogic());
	}

	//******************************************  RECRUITMENT *********************************************/
	private void DoRecruitmentLogic(){
		Debug.Log ("*******IAIAIAIAIAIA************ RECRUITMENT LOGIC ********IAIAIAIAIAIA************");
		// First, check how many regions we have as Nazi
		int numberOfNaziRegions = naziRegions.Count;
		int recruitPointsForThisTurn =  Mathf.FloorToInt(numberOfNaziRegions * baseRecruitPointsPerRegion * 
			turnNumber * (1.0f + recruitPointsMultiplierPerTurn));

		Debug.Log ("IA: Recruit points generated for this turn = " + recruitPointsForThisTurn);

		accumulatedRecruitmentPoints += recruitPointsForThisTurn;

		Debug.Log ("IA: Accumulated recruitment points = " + accumulatedRecruitmentPoints);

		// Decide if perform recruitment or save points for later
		if(DecideIfPerformRecruitment()){
			PerformRecruitment ();
		}
	}

	private bool DecideIfPerformRecruitment(){
		return true;
	}

	private void PerformRecruitment (){
		// Start with the best of the researched units, reversing the list
		researchedArmies.Reverse();

		// Divide the available recruitment points between all Nazi regions 
		int recruitmentPointsPerRegion = accumulatedRecruitmentPoints / naziRegions.Count;

		foreach(Region naziRegion in naziRegions){
			int recruitmentPointsForThisRegion = recruitmentPointsPerRegion;
			Debug.Log ("AIManager: Starting recruiting on Region " + naziRegion);

			// Get always the best unit affordable with the available points
			foreach(Army army in researchedArmies){
				while(recruitmentPointsForThisRegion > army.price && army.price > 0){
					naziRegion.AddUnitsToArmy (army.armyType, 1);
					recruitmentPointsForThisRegion -= army.price;
					accumulatedRecruitmentPoints -= army.price;
					Debug.Log ("AIManager: New unit of type " + army.armyType+ " recruited in region " + 
						naziRegion.name + " Remaining points for this Region = " + recruitmentPointsForThisRegion + 
						" Remaining points in total for this turn = " + accumulatedRecruitmentPoints);
				}
			}
		}

		// Let the list in its previous order
		researchedArmies.Reverse();
	}

	//********************************************  RESEARCH *********************************************/
	private void DoResearchLogic(){
		Debug.Log ("*******IAIAIAIAIAIA************ RESEARCH LOGIC ********IAIAIAIAIAIA************");
		// Loop trough all army types till finding the first non-developed one
		foreach(Army army in FindObjectOfType<ArmyValues>().GetNaziArmies()){
			if(!IsArmyTypeAlreadyResearched(army.armyType)){
				// Once found, decide if we'll research it this turn or not
				// Increase the chance of having a new unit type depending on the turns passed since last research
				if(turnNumber - turnOfLastResearchedArmy > turnsNeededForNextResearch){
					turnOfLastResearchedArmy = turnNumber;
					Debug.Log ("Nazi AI: Nazis just discovered army " + army.armyDescription);
					researchedArmies.Add (army);
				}
				return;
			}
		}
	}

	private bool IsArmyTypeAlreadyResearched(ArmyType type){
		foreach(Army researchedArmy in researchedArmies){
			if(type == researchedArmy.armyType){
				return true;
			}
		}

		return false;
	}

	//******************************************** ATTACK *************************************************/
	private bool DoAttackLogic(Region regionToAttack){
		Debug.Log ("*******IAIAIAIAIAIA************ ATTACK LOGIC ********IAIAIAIAIAIA************");
		CombatManager combatManager = FindObjectOfType<CombatManager> ();
		GameManager gameManager = FindObjectOfType<GameManager> ();

		// We have a spied candidate to attack
		if(regionToAttack != null){
			// Decide here if it's interesting for us to attack the regionToAttack and if we have to move troops
			Region[] borderRegions = gameManager.GetRegionsBorderingRegion(regionToAttack);
			foreach(Region borderRegion in borderRegions){
				if(borderRegion.isNazi){
					// Check if our region has more attack power than the enemy
					if(HasMoreCombatPower(borderRegion, regionToAttack)){
						// Start Attack over the selected Region
						Debug.Log ("Attacking the Republic !!");
						Region attackerRegion = borderRegion;
						Region defenderRegion = regionToAttack;
						combatManager.SetAttackerRegion (attackerRegion);
						combatManager.SetDefenderRegion (defenderRegion);
						combatManager.SetCombatScreenRegions (attackerRegion, defenderRegion);

						// Start the combat
						combatManager.StartCombat(!testMode);

						return true;
					}
				}
			}
		}
		else{
			// No spied region. Decide here if performing a Blind attack or not
		}
			
		return false;
	}

	//******************************************** SPY *************************************************/
	private Region DoSpyLogic(){
		Debug.Log ("*******IAIAIAIAIAIA************ SPY LOGIC ********IAIAIAIAIAIA************");
		GameManager gameManager = FindObjectOfType<GameManager> ();

		// Try to spy an enemy Region
		Region spiedRegion = null;
		List<Region> spiableRegions = new List<Region> ();

		// Add all enemy regions surrounding a Nazi region. Note that some Regions will be duplicated
		// Thus, duplicated regions will have more chances to be spied
		foreach(Region naziRegion in naziRegions){
			foreach(Region borderRegion in gameManager.GetRegionsBorderingRegion(naziRegion)){
				if(!borderRegion.isNazi){
					spiableRegions.Add (borderRegion);
				}
			}
		}

		float chanceOfSuccess = (turnNumber - turnOfLastSuccessfulSpy) / (float)turnsNeededForNextSpy;
		float randomFloat = UnityEngine.Random.Range (0f, 1f);
		Debug.Log ("AIManager = SPYING : chanceOfSuccess: " + chanceOfSuccess + ", randomFloat:" + randomFloat);
		// Decide here if the Spying is successful, chances will increase during turns
		int numberOfSpiableRegions = spiableRegions.Count;
		if(numberOfSpiableRegions>0 && randomFloat < chanceOfSuccess){
			// Success !!
			spiedRegion = spiableRegions[UnityEngine.Random.Range(0, numberOfSpiableRegions-1)];
			Debug.Log ("AIManager = SPY SUCCESS !! => Spied Region : " + spiedRegion);
			turnOfLastSuccessfulSpy = turnNumber;
		}

		return spiedRegion;
	}

	private void RefreshNeededDataForThisTurn(){
		Debug.Log ("*******IAIAIAIAIAIA************ REFRESH DATA FOR AI ********IAIAIAIAIAIA************");
		GameManager gameManager = FindObjectOfType<GameManager> ();
		naziRegions = gameManager.GetAllNaziRegions ();
		turnNumber = gameManager.GetCurrentTurnNumber ();
	}

	private bool HasMoreCombatPower(Region myRegion, Region otherRegion){
		return myRegion.GetCombatPower () > otherRegion.GetCombatPower ();
	}

}