﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class CombatTest {

    [Test]
    public void CombatManagerTest()
    {
		Dictionary<ArmyType, int> attackerArmyDefinition = new Dictionary<ArmyType, int> ();
		attackerArmyDefinition.Add (ArmyType.TankToro, 7);
		attackerArmyDefinition.Add (ArmyType.TankBisonte, 2);
		attackerArmyDefinition.Add (ArmyType.TankLince, 57);

		Dictionary<ArmyType, int> defenderArmyDefinition = new Dictionary<ArmyType, int> ();
		defenderArmyDefinition.Add (ArmyType.Milicia, 6);
		defenderArmyDefinition.Add (ArmyType.TankLince, 5);
		defenderArmyDefinition.Add (ArmyType.TankToro, 5);

        //Create all components
		GameObject combatManager = new GameObject("mockCombatManager");
		combatManager.AddComponent<CombatManager>();

		GameObject attackerRegionGameObject = new GameObject("CabronilandiaRegion");
		attackerRegionGameObject.AddComponent<Region>();
		GameObject defenderRegionGameObject = new GameObject("JoputalandiaRegion");
		defenderRegionGameObject.AddComponent<Region>();

		Region attackerRegion = attackerRegionGameObject.GetComponent<Region> ();
		Region defenderRegion = defenderRegionGameObject.GetComponent<Region> ();

		// Initialize the army of each region
		int maxSlots = attackerRegion.GetMaxSlots();
		attackerRegion.armySlots = new RegionArmySlot[maxSlots];
		attackerRegion.ClearArmySlots ();
		defenderRegion.armySlots = new RegionArmySlot[maxSlots];
		defenderRegion.ClearArmySlots ();

		foreach(KeyValuePair<ArmyType, int> army in attackerArmyDefinition)
		{
			attackerRegion.AddUnitsToArmy (army.Key, army.Value);
		}

		foreach(KeyValuePair<ArmyType, int> army in defenderArmyDefinition)
		{
			defenderRegion.AddUnitsToArmy (army.Key, army.Value);
		}

		// Create and init a new Combat Manager
		CombatManager combatManagerComponent = combatManager.GetComponent<CombatManager> ();
		combatManagerComponent.SetAttackerRegion (attackerRegion);
		combatManagerComponent.SetDefenderRegion (defenderRegion);

		// Start the combat, set waiting to false so 
		combatManagerComponent.StartCombat(false);

		Assert.Pass ();
    }
}
