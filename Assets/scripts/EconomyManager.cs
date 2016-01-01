﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EconomyManager : MonoBehaviour {
	
	public int INITIAL_RECRUITMENT_POINTS;
	public int INITIAL_MAXIMUM_ACTIONS;

	// private static EconomyManager singleton;

	// Recruitment Points available
	private int recruitmentPoints;
	// Remaining actions that can be done this turn
	private int availableActionPointsForThisTurn;
	// Maximum actions per turn (upgradable making action buildings)
	private int maximumActionsPerTurn;

	// Total action generation points of all our Regions
	private int totalActionGenerationPoints;

/*	private static EconomyManager GetSingleton()
	{
		if(singleton == null){
			singleton = new EconomyManager();
		}

		return singleton; 
	} */

	void Awake(){
		recruitmentPoints = INITIAL_RECRUITMENT_POINTS;
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

	public int getRecruitmentPoints(){
		return recruitmentPoints;
	}

	public void addRecruitmentPoints(int amount){
		recruitmentPoints += amount;
	}

	public void decreaseRecruitmentPoints(int amount){
		recruitmentPoints -= amount;
	}

	public bool haveEnoughActionsPoints(int pointsNeeded){
		return availableActionPointsForThisTurn >= pointsNeeded;
	}

	public bool haveEnoughRecruitmentPoints(int pointsNeeded){
		return recruitmentPoints >= pointsNeeded;
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
			FindObjectOfType<PopUpMessages>().ShowDropDownMessageForSecs("You have finished all your actions for this turn", 5f);
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
			FindObjectOfType<PopUpMessages>().ShowDropDownMessageForSecs("COOL !! Now you have " + maximumActionsPerTurn + " Actions per turn !!" , 5f);
		}
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
}
