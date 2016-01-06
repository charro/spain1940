using UnityEngine;

public class Spy: ScriptableObject
{
	public Region spiedRegion;
	public int turnsToEndSpying;
	public int spyLevel;

	public void InitializeSpy(Region newSpiedRegion, int newSpyLevel){
		this.spiedRegion = newSpiedRegion;
		this.spyLevel = newSpyLevel;
		StartSpying ();
	}

	public void StartSpying(){
		turnsToEndSpying = FindObjectOfType<SpyingValues> ().turnsNeededPerLevel[spyLevel-1];
		EventManager.OnPassTurn += PassTurn;
	}

	/**
	void OnEnable()
	{
		EventManager.OnPassTurn += PassTurn;
	}

	void OnDisable()
	{
		EventManager.OnPassTurn -= PassTurn;
	}
	**/

	// This function is called automatically by EventManager when Player pass turn
	public void PassTurn(){
		Debug.Log ("SPY => PASS TURN. Spy of Level " + spyLevel + " in region " + 
		           spiedRegion.ToString() + ". Turns to finish: " + turnsToEndSpying);

		if(turnsToEndSpying > 0){
			turnsToEndSpying--;
			if(turnsToEndSpying == 0){
				SpyingFinished();
			}
		}
	}

	// Do here the actual Spying 
	public void SpyingFinished(){
		SpyingValues spyingValues = FindObjectOfType<SpyingValues> ();

		// Note that we substract 1 to index, because Arrays start with 0 ;)
		float chanceOfFindingArmyType = spyingValues.findArmyTypeChancePerLevel[spyLevel-1];
		float chanceOfFindingArmyAmount = spyingValues.findArmyNumberChancePerLevel[spyLevel-1];

		spiedRegion.RecalculateSpiedInfo (chanceOfFindingArmyType, chanceOfFindingArmyAmount);

		FindObjectOfType<DropDownMessages> ().ShowDropDownMessageForSecs ("Spying in Region " + spiedRegion.name + " finished", 5);

		EventManager.OnPassTurn -= PassTurn;

		FindObjectOfType<SpyManager> ().RemoveSpy (this);
	}

}