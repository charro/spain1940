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
			List<RegionArmySlot> thisTurnAttackerArmy;
			List<RegionArmySlot> thisTurnDefenderArmy;
			Dictionary<ArmyType, int> thisTurnLosses;

			// Decide which army is attacking and which one is defending depending on the turn
			if (attackerRegionTurn) {
				thisTurnAttackerArmy = nonEmptyAttackerRegionSlots;
				thisTurnDefenderArmy = nonEmptyDefenderRegionSlots;
				thisTurnLosses = defenderRegionLosses;
			} else {
				thisTurnAttackerArmy = nonEmptyDefenderRegionSlots;
				thisTurnDefenderArmy = nonEmptyAttackerRegionSlots;
				thisTurnLosses = attackerRegionLosses;
			}

			// Start attacking randomly
			foreach(RegionArmySlot attackerUnit in thisTurnAttackerArmy){
				Debug.Log ("Combat in progress. Turn number: " + combatTurnsPassed + " | Attacker Turn? " + attackerRegionTurn);

				// Choose a Random unit to be attacked
				int targetUnit = Random.Range(0, thisTurnDefenderArmy.Count);
				Debug.Log ("Attacker: " + (attackerRegionTurn ? attackerRegion : defenderRegion) + 
					" attacking unit:" + attackerUnit);
				RegionArmySlot defendingUnit = thisTurnDefenderArmy[targetUnit];
				Debug.Log ("Defender: " + (attackerRegionTurn ? defenderRegion : attackerRegion) + 
					" defending unit:" + defendingUnit);

				// Kill Unit
				defendingUnit.removeUnit ();
				if(defendingUnit.armyAmount == 0){
					Debug.Log ("Unit Type: " + defendingUnit.armyType + " is DESTROYED !! ");
					thisTurnDefenderArmy.Remove (defendingUnit);
				}

				int lossesAlready = 0;
				if(thisTurnLosses.ContainsKey(defendingUnit.armyType)){
					lossesAlready = thisTurnLosses[defendingUnit.armyType];
				}
				thisTurnLosses[defendingUnit.armyType] = lossesAlready+1;

				// Pass turn
				attackerRegionTurn = !attackerRegionTurn;
				combatTurnsPassed++;
			}
		}
			
		if (attackerRegion.HasAnyTroops ()) {
			Debug.Log (attackerRegion + " WON !!");
		} else {
			Debug.Log (defenderRegion + " WON !!");
		}
	}
		
}