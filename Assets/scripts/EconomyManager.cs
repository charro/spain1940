using UnityEngine;
using System.Collections;

public class EconomyManager : MonoBehaviour {
	
	public int INITIAL_RECRUITMENT_POINTS;
	public int INITIAL_MAXIMUM_ACTIONS;

	// private static EconomyManager singleton;

	// Recruitment Points available
	private int recruitmentPoints;
	// Remaining actions that can be done this turn
	private int availableTurnActions;
	// Maximum actions per turn (upgradable by new technologies)
	private int maximumActionsPerTurn;

/*	private static EconomyManager GetSingleton()
	{
		if(singleton == null){
			singleton = new EconomyManager();
		}

		return singleton; 
	} */

	// Use this for initialization
	void Start () {
		//singleton = this;
		recruitmentPoints = INITIAL_RECRUITMENT_POINTS;
		maximumActionsPerTurn = INITIAL_MAXIMUM_ACTIONS;
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

	public int getAvailableActions(){
		return availableTurnActions;
	}

	public void decreaseActionPoints(int amount){
		if(availableTurnActions < amount){
			Debug.Log("WARNING: Trying to decrease " + amount + " Action points when only " + availableTurnActions + " available");
			availableTurnActions = 0;
		}
		else{
			availableTurnActions -= amount;
		}
	}

	public void resetAvailableActions(){
		availableTurnActions = maximumActionsPerTurn;
	}
}
