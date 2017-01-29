using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatResult : MonoBehaviour {

	public bool attackerWon;
	public Dictionary<ArmyType, int> attackerRegionLosses;
	public Dictionary<ArmyType, int> defenderRegionLosses;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	void SetAttackerWon(bool won){
		attackerWon = won;
	}

	void SetAttackerLosses(Dictionary<ArmyType, int> losses){
		attackerRegionLosses = losses;
	}

	void SetDefenderLosses(Dictionary<ArmyType, int> losses){
		defenderRegionLosses = losses;
	}*/

	public void RefreshLastCombatData(bool won, Dictionary<ArmyType, int> attackerLosses, 
		Dictionary<ArmyType, int> defenderLosses){

		attackerWon = won;
		attackerRegionLosses = attackerLosses;
		defenderRegionLosses = defenderLosses;
	}
}
