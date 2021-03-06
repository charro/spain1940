using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EconomyManager : MonoBehaviour {
	
	public int INITIAL_MILITARY_POINTS;
	public int INITIAL_MAXIMUM_ACTIONS;

	// private static EconomyManager singleton;

	// Recruitment Points available
	private int militaryPoints;
	// Remaining actions that can be done this turn
	private int availableActionPointsForThisTurn;
	// Maximum actions per turn (upgradable making action buildings)
	private int maximumActionsPerTurn;

	// Total action generation points of all our Regions
	private int totalActionGenerationPoints;
	// Total military generation points for all of our Regions
	private int totalMilitaryGenerationPoints;

/*	private static EconomyManager GetSingleton()
	{
		if(singleton == null){
			singleton = new EconomyManager();
		}

		return singleton; 
	} */

	void Awake(){
		militaryPoints = INITIAL_MILITARY_POINTS;
		maximumActionsPerTurn = INITIAL_MAXIMUM_ACTIONS;
		resetAvailableActions ();
	}

	// Use this for initialization
	void Start () {
		//singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getMilitaryPoints(){
		return militaryPoints;
	}

	public void addMilitaryPoints(int amount){
		militaryPoints += amount;
	}

	public void decreaseMilitaryPoints(int amount){
		militaryPoints -= amount;
	}

	public int GetNewActionsPerTurn (){
		return maximumActionsPerTurn;
	}

	public int GetNewRecPointsPerTurn(){
		return totalMilitaryGenerationPoints;
	}

	// Get the amount of military points generated by all regions and add it
	public void IncreaseMilitaryPointsForNextTurn(){
		addMilitaryPoints (totalMilitaryGenerationPoints);
	}

	public int GetTotalMilitaryGenerationPoints(){
		return totalMilitaryGenerationPoints;
	}

	public bool haveEnoughActionsPoints(int pointsNeeded){
		return availableActionPointsForThisTurn >= pointsNeeded;
	}

	public bool haveEnoughRecruitmentPoints(int pointsNeeded){
		return militaryPoints >= pointsNeeded;
	}

	public bool isAnyActionPointAvailable(){
		return availableActionPointsForThisTurn > 0;
	}

	public int getAvailableActionPoints(){
		return availableActionPointsForThisTurn;
	}

	public int GetTotalActionGenerationPoints(){
		return totalActionGenerationPoints;
	}

	public void decreaseActionPoints(int amount){
		if(availableActionPointsForThisTurn < amount){
			Debug.Log("WARNING: Trying to decrease " + amount + " Action points when only " + availableActionPointsForThisTurn + " available");
			availableActionPointsForThisTurn = 0;
		}
		else{
			availableActionPointsForThisTurn -= amount;
		}

		if(availableActionPointsForThisTurn == 0){
			FindObjectOfType<DropDownMessages>().ShowDropDownMessageForSecs("You have finished all your actions for this turn", 5f);
			EventManager.TriggerNoMoreActionsEvent();
		}
	}

	public void resetAvailableActions(){
		availableActionPointsForThisTurn = maximumActionsPerTurn;
	}

	// After any change in region buildings, recalculate the maximum points
	public void recalculateMaximumActionsPerTurn(){
		BuildValues buildValues = FindObjectOfType<BuildValues> ();
		Dictionary<RegionType, Region> allRegions = FindObjectOfType<GameManager> ().GetAllRegions ();

		int accumulatedActionGenerationPointsOfAllRegions = 0;

		 foreach(RegionType regionType in allRegions.Keys){
			Region region = allRegions[regionType];
			if(!region.isNazi){
				accumulatedActionGenerationPointsOfAllRegions += region.GetActionGenerationPoints();
			}
		}

		totalActionGenerationPoints = accumulatedActionGenerationPointsOfAllRegions;

		// Once all points from all regions are accumulated, check which Treshold are we reaching with it
		int actionPointsToAdd = 0;

		for(; actionPointsToAdd < buildValues.actionGenerationPointsThresholds.Length; actionPointsToAdd++){
			if(accumulatedActionGenerationPointsOfAllRegions < buildValues.actionGenerationPointsThresholds[actionPointsToAdd]){
				break;
			}
		}

		// We add the minimum to the recalculated amount and show a message if we increased max Action Points
		int oldMaximum = maximumActionsPerTurn;
		maximumActionsPerTurn = actionPointsToAdd + INITIAL_MAXIMUM_ACTIONS;

		if(maximumActionsPerTurn > oldMaximum){
			FindObjectOfType<DropDownMessages>().ShowDropDownMessageForSecs("COOL !! Now you have " + maximumActionsPerTurn + " Actions per turn !!" , 5f);
		}
	}

	public void RecalculateTotalMilitaryGenerationPoints(){
		List<Region> republicanRegions = FindObjectOfType<GameManager> ().GetAllRepublicanRegions();

		int militaryPointsToIncrease = 0;
		foreach(Region region in republicanRegions){
			militaryPointsToIncrease += region.GetMilitaryPointsGeneration ();
		}

		totalMilitaryGenerationPoints = militaryPointsToIncrease;
	}

	public int GetActionPointsThresholdForNextLevel(){
		BuildValues buildValues = FindObjectOfType<BuildValues> ();
		int currentTotalActionPoints = GetTotalActionGenerationPoints ();

		foreach(int threshold in buildValues.actionGenerationPointsThresholds){
			if(currentTotalActionPoints < threshold){
				return threshold;
			}
		}

		// Not found, just return 0 => This should never happen
		return 0;
	}

	public void RestoreDataFromSaveGame(SaveGameManager.SaveGameData gameData){
		militaryPoints = gameData.militaryPoints;
		availableActionPointsForThisTurn = gameData.availableActionPointsForThisTurn;
		maximumActionsPerTurn = gameData.maximumActionsPerTurn;
		totalActionGenerationPoints = gameData.totalActionGenerationPoints;
		totalMilitaryGenerationPoints = gameData.totalMilitaryGenerationPoints;
	}

	public void FillSaveGameData(SaveGameManager.SaveGameData gameData){
		gameData.militaryPoints = militaryPoints;
		gameData.availableActionPointsForThisTurn = availableActionPointsForThisTurn;
		gameData.maximumActionsPerTurn = maximumActionsPerTurn;
		gameData.totalActionGenerationPoints = totalActionGenerationPoints;
		gameData.totalMilitaryGenerationPoints = totalMilitaryGenerationPoints;
	}
}
