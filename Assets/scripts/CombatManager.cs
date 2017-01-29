using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {

	public float DELAY_ON_SHOW = 0.5f;

	// Screen to show the combat
	public CombatScreen combatScreen;
	// Screen post-combat
	public CombatResultPanel combatResultPanel;
	// To show if the Region changed
	public RegionChangedPanel regionChangedPanel;

	private Region attackerRegion;
	private Region defenderRegion;

	private int combatTurnsPassed = 0;
	private bool attackerRegionTurn = true;

	private List<RegionArmySlot> nonEmptyAttackerRegionSlots;
	private List<RegionArmySlot> nonEmptyDefenderRegionSlots;

	private Dictionary<ArmyType, int> attackerRegionLosses;
	private Dictionary<ArmyType, int> defenderRegionLosses;

	private bool attackerWins = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDefenderRegion(Region region){
		defenderRegion = region;
	}

	public void SetAttackerRegion(Region region){
		attackerRegion = region;
	}

	public Region GetDefenderRegion(){
		return defenderRegion;
	}

	public Region GetAttackerRegion(){
		return attackerRegion;
	}

	public void StartCombat(bool wait){
		// If the player started the attack, decrease the corresponding action point
		if(!attackerRegion.isNazi){
			FindObjectOfType<EconomyManager>().decreaseActionPoints(1);
		}

		if(wait){
			combatScreen.gameObject.SetActive (true);
			UIManager.hideAllPanels ();
		}
		StartCoroutine(CalculateCombat(wait));
	}

	public void ShowConfirmStartAttackFrom(Region region){
		// The attacking region is the just clicked region
		attackerRegion = region;
		SetCombatScreenRegions (attackerRegion, defenderRegion);
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmAttack);
	}

	public void SetCombatScreenRegions(Region attackerRegion, Region defenderRegion){
		if (attackerRegion.isNazi) {
			combatScreen.SetCombatRegions (defenderRegion, attackerRegion);
		} else {
			combatScreen.SetCombatRegions (attackerRegion, defenderRegion);
		}

	}

	// waitForIt=false lets you call this method from Unit Tests
	IEnumerator CalculateCombat(bool waitForIt){
		Debug.Log ("STARTING NEW COMBAT !! : " + attackerRegion + " ATTACKS " + defenderRegion);
			
		combatTurnsPassed = 0;
		attackerRegionTurn = true;

		nonEmptyAttackerRegionSlots = new List<RegionArmySlot> ();
		nonEmptyDefenderRegionSlots = new List<RegionArmySlot> ();

		// Add only non-empty slots to the battle
		foreach(RegionArmySlot armySlot in attackerRegion.armySlots){
			if(armySlot != null && armySlot.armyType != ArmyType.Empty && armySlot.armyAmount > 0){
				nonEmptyAttackerRegionSlots.Add (armySlot);
			}
		}

		foreach(RegionArmySlot armySlot in defenderRegion.armySlots){
			if(armySlot != null && armySlot.armyType != ArmyType.Empty && armySlot.armyAmount > 0){
				nonEmptyDefenderRegionSlots.Add (armySlot);
			}
		}

		attackerRegionLosses = new Dictionary<ArmyType, int> ();
		defenderRegionLosses = new Dictionary<ArmyType, int> ();



		// Loop until one of the opponents lose all of his units
		while(attackerRegion.HasAnyTroops() && defenderRegion.HasAnyTroops()){
			Debug.Log ("Combat in progress. TURN NUMBER: " + combatTurnsPassed + " | Attacker Turn? " + attackerRegionTurn +
				"\n********************************************************************************************************");

			List<RegionArmySlot> thisTurnAttackerArmy;
			List<RegionArmySlot> thisTurnDefenderArmy;
			Dictionary<ArmyType, int> thisTurnLosses;
			Region thisTurnAttackerRegion;
			Region thisTurnDefenderRegion;

			// Decide which army is attacking and which one is defending depending on the turn
			if (attackerRegionTurn) {
				thisTurnAttackerArmy = nonEmptyAttackerRegionSlots;
				thisTurnDefenderArmy = nonEmptyDefenderRegionSlots;
				thisTurnLosses = defenderRegionLosses;
				thisTurnAttackerRegion = attackerRegion;
				thisTurnDefenderRegion = defenderRegion;
			} else {
				thisTurnAttackerArmy = nonEmptyDefenderRegionSlots;
				thisTurnDefenderArmy = nonEmptyAttackerRegionSlots;
				thisTurnLosses = attackerRegionLosses;
				thisTurnAttackerRegion = defenderRegion;
				thisTurnDefenderRegion = attackerRegion;
			}

			// Start attacking one by one with all the attacker Units
			foreach(RegionArmySlot nextAttackerUnit in thisTurnAttackerArmy){
				
				Debug.Log ("Attacker: " + thisTurnAttackerRegion + " attacking unit:" + nextAttackerUnit);

				if(thisTurnDefenderArmy.Count == 0){
					Debug.Log ("No more defending units. " +  thisTurnDefenderRegion + " is defeated");
					break;
				}

				// Decide here if attacker missed or succeced
				int chanceOfSuccess = 85;

				// SUCCESS ON THE ATTACK
				if (Random.Range (0, 100) < chanceOfSuccess) {

					// Choose here a unit to be attacked

					// Random choosing
					int targetUnit = Random.Range (0, thisTurnDefenderArmy.Count);
					RegionArmySlot defendingUnit = thisTurnDefenderArmy [targetUnit];
					Debug.Log ("Defender: " + thisTurnDefenderRegion + " defending unit:" + defendingUnit);

					// Perform the attack on the defending unit
					ArmyType defendingUnitType = defendingUnit.armyType;
					if (waitForIt) {
						// Here, animation for attacker unit
						combatScreen.ShowShooting(nextAttackerUnit.armyType);
						yield return new WaitForSeconds (DELAY_ON_SHOW);
					}

					int unitsKilled = CalculateUnitsKilled (nextAttackerUnit, defendingUnit, waitForIt);

					// Kill Unit
					defendingUnit.removeUnits(unitsKilled);

					// UNIT DESTROYED
					if (defendingUnit.armyAmount == 0) {
						Debug.Log ("Unit Type: " + defendingUnitType + " of region " +
						thisTurnDefenderRegion + " is DESTROYED !! ");
						thisTurnDefenderArmy.Remove (defendingUnit);
						if (waitForIt) {
							yield return new WaitForSeconds (DELAY_ON_SHOW);
						}
					}

					// Add unit killed to this region losses
					int lossesAlready = 0;
					if (thisTurnLosses.ContainsKey (defendingUnitType)) {
						lossesAlready = thisTurnLosses [defendingUnitType];
					}
					thisTurnLosses [defendingUnitType] = lossesAlready + unitsKilled;
				}
				// ATTACK MISSED
				else {
					Debug.Log ("ATTACK MISSED !!");
					/* if(waitForIt){
						combatScreen.ShowMissedUnitMessage (attackerRegion.isNazi, attackerUnit.armyType, 1.5f);
						yield return new WaitForSeconds (1.5f);
					}*/
				}

				Debug.Log ("----------------------------------------------------------------------");
			}

			// Pass turn
			attackerRegionTurn = !attackerRegionTurn;
			combatTurnsPassed++;

			// Put a yield here just before the end of the loop, to avoid hunging Unity
			if(waitForIt){
				yield return new WaitForEndOfFrame ();
			}
		}

		Debug.Log ("COMBAT FINISHED - SUMMARY OF THE COMBAT"+
			"\n****************************************************************************"+
			"\n****************************************************************************");

		if (attackerRegion.HasAnyTroops ()) {
			Debug.Log (attackerRegion + " WON !!");
			attackerWins = true;
		} else {
			Debug.Log (defenderRegion + " WON !!");
			attackerWins = false;
		}

		Debug.Log ("Losses of Attacking Region " + attackerRegion);
		foreach(KeyValuePair<ArmyType, int> army in attackerRegionLosses){
			Debug.Log (army.Key + " = " + army.Value);
		}

		Debug.Log ("Losses of Defending Region " + defenderRegion);
		foreach(KeyValuePair<ArmyType, int> army in defenderRegionLosses){
			Debug.Log (army.Key + " = " + army.Value);
		}

		// Finish and Show the combat result screen
		if(waitForIt){
			FinishCombat ();

			bool naziWon = (attackerRegion.isNazi && attackerWins) || (defenderRegion.isNazi && !attackerWins);

			if (attackerRegion.isNazi) {
				combatResultPanel.ShowPanel (defenderRegionLosses, attackerRegionLosses, naziWon);
			} 
			else {
				combatResultPanel.ShowPanel (attackerRegionLosses, defenderRegionLosses, naziWon);
			}
		}
	}

	// Used to avoid the combat to enter in an infinite
	Dictionary<ArmyType, int> accumulatedDamage = new Dictionary<ArmyType, int> ();

	public int CalculateUnitsKilled(RegionArmySlot attackerUnit, RegionArmySlot defenderUnit, bool waitForIt){
		ArmyValues armyValues = FindObjectOfType<ArmyValues> ();
		Army attackerArmy = armyValues.GetArmy (attackerUnit.armyType);
		Army defenderArmy = armyValues.GetArmy (defenderUnit.armyType);

		int attackerUnits = attackerUnit.armyAmount;
		int defenderUnits = defenderUnit.armyAmount;

		// Total damage made by attacker troops
		int totalAttackerDamage = attackerUnits * attackerArmy.attack;

		// Calculate damage reduction factor of defender, based on the defense of defender (Limited to 90% of damage)
		float damageReduction = (Mathf.Pow(1.05f, (float)defenderArmy.defense)) - 1;
		damageReduction = (damageReduction > 0.9f ? 0.9f : damageReduction);

		// Recalculate the total damage with the damage reduction
		totalAttackerDamage =  (int)(totalAttackerDamage * (1 - damageReduction));

		int defenderLifePerUnit = defenderArmy.defense * 10;

		Debug.Log ("Attacker unit total damage after reduction of the " + damageReduction + 
			" : " + totalAttackerDamage);

		if(waitForIt){
			combatScreen.ShowDamagePointsMessage (defenderArmy.armyType, totalAttackerDamage);
		}

		int totalLostUnits = (totalAttackerDamage / defenderLifePerUnit);

		// In case of not enough damage to destroy a unit, accumulate the damage (to avoid never killing it)
		if(totalLostUnits==0){
			int currentDamage = 0;
			if(accumulatedDamage.ContainsKey(defenderArmy.armyType)){
				currentDamage = accumulatedDamage[defenderArmy.armyType];
			}

			int newAccumulatedDamage = currentDamage + totalAttackerDamage;
			accumulatedDamage [defenderArmy.armyType] = newAccumulatedDamage;

			if(newAccumulatedDamage >= defenderLifePerUnit){
				Debug.Log ("Accumulated damage " + newAccumulatedDamage + " for this unit is higher than the unit life. One unit killed");		
				totalLostUnits = 1;
				accumulatedDamage [defenderArmy.armyType] = 0;
			}
		}

		Debug.Log ("Life per defender unit: " + defenderLifePerUnit + " | Defender units destroyed: " + totalLostUnits);

		return totalLostUnits;
	}

	public void FinishCombat(){

		if(attackerWins){
			regionChangedPanel.Show (defenderRegion);  // => IMPORTANT. CALL THIS FIRST (DONT CHANGE TO NAZI UNTIL IS CALLED)
			defenderRegion.SetNaziConquered (attackerRegion.isNazi);
		}

		combatScreen.gameObject.SetActive (false);

		// Check for any change in Economy after losing/winning a Region
		FindObjectOfType<EconomyManager> ().recalculateMaximumActionsPerTurn ();

		// End combat and back to Idle map
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
	}

	public void ResultsScreenClosed(){

		if (attackerRegion.isNazi) {
			UIManager.ShowLoadingTmp ();
		}
	}
}