using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class CombatTest {

    [Test]
    public void CombatManagerTest()
    {
		Dictionary<ArmyType, int> attackerArmyDefinition = new Dictionary<ArmyType, int> ();
		attackerArmyDefinition.Add (ArmyType.TankToro, 17);
		attackerArmyDefinition.Add (ArmyType.TankLince, 15);

		Dictionary<ArmyType, int> defenderArmyDefinition = new Dictionary<ArmyType, int> ();
		defenderArmyDefinition.Add (ArmyType.Milicia, 160);
		defenderArmyDefinition.Add (ArmyType.TankLince, 5);
		defenderArmyDefinition.Add (ArmyType.TankToro, 5);

        //Create all components
		GameObject combatManager = new GameObject("mockCombatManager");
		combatManager.AddComponent<CombatManager>();

		GameObject attackerRegionGameObject = new GameObject("CabronilandiaRegion");
		attackerRegionGameObject.AddComponent<Region>();
		GameObject defenderRegionGameObject = new GameObject("JoputalandiaRegion");
		defenderRegionGameObject.AddComponent<Region>();

		// Create all army types
		GameObject tankToroArmy = new GameObject("tankToroArmy");
		tankToroArmy.AddComponent<Army>();
		Army tankToroArmyComponent = tankToroArmy.GetComponent<Army> ();
		tankToroArmyComponent.armyType = ArmyType.TankToro;
		tankToroArmyComponent.attack = 3;
		tankToroArmyComponent.defense = 7;
		tankToroArmyComponent.speed = 2;

		GameObject tankLinceArmy = new GameObject("tankLinceArmy");
		tankLinceArmy.AddComponent<Army>();
		Army tankLinceArmyComponent = tankLinceArmy.GetComponent<Army> ();
		tankLinceArmyComponent.armyType = ArmyType.TankLince;
		tankLinceArmyComponent.attack = 2;
		tankLinceArmyComponent.defense = 5;
		tankLinceArmyComponent.speed = 6;

		GameObject miliciaArmy = new GameObject("miliciaArmy");
		miliciaArmy.AddComponent<Army>();
		Army miliciaArmyComponent = miliciaArmy.GetComponent<Army> ();
		miliciaArmyComponent.armyType = ArmyType.Milicia;
		miliciaArmyComponent.attack = 1;
		miliciaArmyComponent.defense = 1;
		miliciaArmyComponent.speed = 1;

		// Add them to the armies dictionary and then to the ArmyValues object
		Dictionary<ArmyType, Army> armiesDictionary = new Dictionary<ArmyType, Army>();
		armiesDictionary.Add (tankToroArmyComponent.armyType, tankToroArmyComponent);
		armiesDictionary.Add (tankLinceArmyComponent.armyType, tankLinceArmyComponent);
		armiesDictionary.Add (miliciaArmyComponent.armyType, miliciaArmyComponent);

		GameObject armyValues = new GameObject("ArmyValues");
		armyValues.AddComponent<ArmyValues>();
		armyValues.GetComponent<ArmyValues> ().SetArmyDictionary (armiesDictionary);

		// Create the regions
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

		Debug.Log ("CombatTest: Starting the combat !!");

		// Create and init a new Combat Manager
		CombatManager combatManagerComponent = combatManager.GetComponent<CombatManager> ();
		combatManagerComponent.SetAttackerRegion (attackerRegion);
		combatManagerComponent.SetDefenderRegion (defenderRegion);

		// Start the combat, set waiting to false so only the result is calculated (no visual feedback)
		combatManagerComponent.StartCombat(false);

		Assert.Pass ();
    }
}
