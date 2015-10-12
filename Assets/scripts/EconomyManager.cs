using UnityEngine;
using System.Collections;

public class EconomyManager : MonoBehaviour {
	
	public int INITIAL_RECRUITMENT_POINTS;
	public int INITIAL_MAXIMUM_ACTIONS;

	// private static EconomyManager singleton;

	// Recruitment Points available
	private int recruitmentPoints;
	// Remaining actions that can be done this turn
	private int availableActionPointsForThisTurn;
	// Maximum actions per turn (upgradable by new technologies)
	private int maximumActionsPerTurn;

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

	public bool haveEnoughActionsPoints(int points){
		return availableActionPointsForThisTurn >= points;
	}

	public int getAvailableActionPoints(){
		return availableActionPointsForThisTurn;
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
			FindObjectOfType<HUD>().ShowDropDownMessageForSecs("You have finished all your actions for this turn", 5f);
		}
	}

	public void resetAvailableActions(){
		availableActionPointsForThisTurn = maximumActionsPerTurn;
	}
}
