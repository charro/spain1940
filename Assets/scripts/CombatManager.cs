using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {

	// Screen to show the combat
	public CombatScreen combatScreen;

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
		// IN DEVELOPMENT. BY THE MOMENT, AUTOMATIC WIN
		if(wait){
			combatScreen.gameObject.SetActive (true);
			UIManager.hideAllPanels ();
		}
		StartCoroutine(CalculateCombat(wait));
	}

	public void ShowConfirmStartAttackFrom(Region region){
		// The attacking region is the just clicked region
		attackerRegion = region;
		combatScreen.SetCombatRegions (attackerRegion, defenderRegion);
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmAttack);
	}

	IEnumerator CalculateCombat(bool wait){
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

			// Start attacking randomly with all the attacker Units
			foreach(RegionArmySlot attackerUnit in thisTurnAttackerArmy){
				
				Debug.Log ("Attacker: " + thisTurnAttackerRegion + " attacking unit:" + attackerUnit);

				if(thisTurnDefenderArmy.Count == 0){
					Debug.Log ("No more defending units. " +  thisTurnDefenderRegion + " is defeated");
					break;
				}

				// Choose a Random unit to be attacked
				int targetUnit = Random.Range(0, thisTurnDefenderArmy.Count);
				RegionArmySlot defendingUnit = thisTurnDefenderArmy[targetUnit];
				Debug.Log ("Defender: " + thisTurnDefenderRegion + " defending unit:" + defendingUnit);

				// Perform the attack on the defending unit
				ArmyType defendingUnitType = defendingUnit.armyType;
				if(wait){
					yield return new WaitForSeconds(1);
				}

				// Kill Unit
				defendingUnit.removeUnit ();

				// UNIT DESTROYED
				if(defendingUnit.armyAmount == 0){
					Debug.Log ("Unit Type: " + defendingUnitType + " of region " + 
						thisTurnDefenderRegion + " is DESTROYED !! ");
					thisTurnDefenderArmy.Remove (defendingUnit);
					if(wait){
						yield return new WaitForSeconds(1);
					}
				}

				// Add unit killed to this region losses
				int lossesAlready = 0;
				if(thisTurnLosses.ContainsKey(defendingUnitType)){
					lossesAlready = thisTurnLosses[defendingUnitType];
				}
				thisTurnLosses[defendingUnitType] = lossesAlready+1;

				Debug.Log ("----------------------------------------------------------------------");
			}

			// Pass turn
			attackerRegionTurn = !attackerRegionTurn;
			combatTurnsPassed++;
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

		FinishCombat ();
	}

	public void FinishCombat(){
		if(attackerWins){
			defenderRegion.SetNaziConquered (attackerRegion.isNazi);
		}

		combatScreen.gameObject.SetActive (false);

		FindObjectOfType<EconomyManager>().decreaseActionPoints(1);
		// End combat and back to Idle map
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
	}
}