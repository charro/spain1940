using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class AITest {

    [Test]
    public void AIManagerTest()
    {
		/************************************** CREATE AI MANAGER AND REGIONS *****************************/
		GameObject aiManager = new GameObject("mockAIManager");
		aiManager.AddComponent<AIManager>();
		aiManager.GetComponent<AIManager>().baseRecruitPointsPerRegion = 20;
		aiManager.GetComponent<AIManager>().recruitPointsMultiplierPerTurn = 0.1f;

		GameObject gameManager = new GameObject("mockGameManager");
		gameManager.AddComponent<GameManager>();

		GameObject regionNazi1 = new GameObject("regionNazi1");
		regionNazi1.AddComponent<Region>();
		regionNazi1.GetComponent<Region> ().isNazi = true;
		regionNazi1.GetComponent<Region> ().regionType = RegionType.Galicia;

		GameObject regionNazi2 = new GameObject("regionNazi2");
		regionNazi2.AddComponent<Region>();
		regionNazi2.GetComponent<Region> ().isNazi = true;
		regionNazi2.GetComponent<Region> ().regionType = RegionType.Asturias;

		GameObject regionNoNazi = new GameObject("regionNoNazi");
		regionNoNazi.AddComponent<Region>();
		regionNoNazi.GetComponent<Region> ().regionType = RegionType.Leon;

		gameManager.GetComponent<GameManager>().AddRegionToList(regionNazi1.GetComponent<Region> ());
		gameManager.GetComponent<GameManager>().AddRegionToList(regionNazi2.GetComponent<Region> ());
		gameManager.GetComponent<GameManager>().AddRegionToList(regionNoNazi.GetComponent<Region> ());

		/********************************************* CREATE TEST ARMIES  ************************************/

		// Create all army types
		GameObject naziArmy1 = new GameObject("naziArmy1");
		naziArmy1.AddComponent<Army>();
		Army naziArmyComponent = naziArmy1.GetComponent<Army> ();
		naziArmyComponent.armyType = ArmyType.NaziJager;
		naziArmyComponent.armyDescription = "Nazi Jager Soldier";
		naziArmyComponent.attack = 3;
		naziArmyComponent.defense = 7;
		naziArmyComponent.speed = 2;

		GameObject naziArmy2 = new GameObject("naziArmy2");
		naziArmy2.AddComponent<Army>();
		Army naziArmyComponent2 = naziArmy2.GetComponent<Army> ();
		naziArmyComponent2.armyType = ArmyType.NaziMeBf109;
		naziArmyComponent.armyDescription = "Nazi Me Bf109";
		naziArmyComponent2.attack = 6;
		naziArmyComponent2.defense = 4;
		naziArmyComponent2.speed = 5;

		// Add them to the armies dictionary and then to the ArmyValues object
		Dictionary<ArmyType, Army> armiesDictionary = new Dictionary<ArmyType, Army>();
		armiesDictionary[naziArmyComponent.armyType] = naziArmyComponent;
		armiesDictionary[naziArmyComponent2.armyType] = naziArmyComponent2;

		List<Army> naziArmies = new List<Army>();
		naziArmies.Add (naziArmyComponent);
		naziArmies.Add (naziArmyComponent2);

		GameObject armyValues = new GameObject("ArmyValues");
		armyValues.AddComponent<ArmyValues>();
		armyValues.GetComponent<ArmyValues> ().SetArmyDictionary (armiesDictionary);
		armyValues.GetComponent<ArmyValues> ().SetNaziArmiesList (naziArmies);

		/*********************************************  TEST CODE ***************************************/

		Debug.Log ("AITest: Starting the Test !!");

		for(int turn=0; turn<5; turn++){
			aiManager.GetComponent<AIManager> ().DoAITurnActions ();
			Debug.Log ("AITest: Finishing turn " + turn);
			gameManager.GetComponent<GameManager> ().IncreaseCurrentTurn ();
		}

		/*
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
		*/

		Assert.Pass ();
    }
}
