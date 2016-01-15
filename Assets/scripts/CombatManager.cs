using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {

	private Region attackerRegion;
	private Region defenderRegion;

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

	public void StartCombat(){
		// IN DEVELOPMENT. BY THE MOMENT, AUTOMATIC WIN
		CalculateCombat();
		defenderRegion.SetNaziConquered (false);

		FindObjectOfType<EconomyManager>().decreaseActionPoints(1);
		// End combat and back to Idle map
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
	}

	public void ConfirmStartAttackFrom(Region region){
		// The attacking region is the just clicked region
		attackerRegion = region;

		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmAttack);
	}

	public void CalculateCombat(){
		Debug.Log ("STARTING NEW COMBAT !! : " + attackerRegion + " ATTACKS " + defenderRegion);
			
		int combatTurnsPassed = 0;
		bool attackerRegionTurn = true;

		List<RegionArmySlot> nonEmptyAttackerRegionSlots = new List<RegionArmySlot> ();
		List<RegionArmySlot> nonEmptyDefenderRegionSlots = new List<RegionArmySlot> ();

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

		Dictionary<ArmyType, int> attackerRegionLosses = new Dictionary<ArmyType, int> ();
		Dictionary<ArmyType, int> defenderRegionLosses = new Dictionary<ArmyType, int> ();

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
					Debug.Log ("No more defending units. is defeated");
					break;
				}

				// Choose a Random unit to be attacked
				int targetUnit = Random.Range(0, thisTurnDefenderArmy.Count);
				RegionArmySlot defendingUnit = thisTurnDefenderArmy[targetUnit];
				Debug.Log ("Defender: " + thisTurnDefenderRegion + " defending unit:" + defendingUnit);

				// Kill Unit
				ArmyType defendingUnitType = defendingUnit.armyType;
				defendingUnit.removeUnit ();
				if(defendingUnit.armyAmount == 0){
					Debug.Log ("Unit Type: " + defendingUnitType + " of region " + 
						thisTurnDefenderRegion + " is DESTROYED !! ");
					thisTurnDefenderArmy.Remove (defendingUnit);
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
		} else {
			Debug.Log (defenderRegion + " WON !!");
		}

		Debug.Log ("Losses of Attacking Region " + attackerRegion);
		foreach(KeyValuePair<ArmyType, int> army in attackerRegionLosses){
			Debug.Log (army.Key + " = " + army.Value);
		}

		Debug.Log ("Losses of Defending Region " + defenderRegion);
		foreach(KeyValuePair<ArmyType, int> army in defenderRegionLosses){
			Debug.Log (army.Key + " = " + army.Value);
		}
	}
		
}