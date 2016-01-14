using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CombatTest {

    [Test]
    public void CombatManagerTest()
    {
        //Create all components
		GameObject combatManager = new GameObject("mockCombatManager");
		combatManager.AddComponent<CombatManager>();

		GameObject attackerRegion = new GameObject("attackerRegion");
		attackerRegion.AddComponent<Region>();
		GameObject defenderRegion = new GameObject("defenderRegion");
		defenderRegion.AddComponent<Region>();

		attackerRegion.GetComponent<Region> ().AddUnitsToArmy (ArmyType.Milicia, 33);
		attackerRegion.GetComponent<Region> ().AddUnitsToArmy (ArmyType.TankBisonte, 15);

		defenderRegion.GetComponent<Region> ().AddUnitsToArmy (ArmyType.TankToro, 13);
		defenderRegion.GetComponent<Region> ().AddUnitsToArmy (ArmyType.TankLince, 1);

		CombatManager combatManagerComponent = combatManager.GetComponent<CombatManager> ();
		combatManagerComponent.SetAttackerRegion (attackerRegion.GetComponent<Region> ());
		combatManagerComponent.SetDefenderRegion (defenderRegion.GetComponent<Region> ());

		combatManagerComponent.CalculateCombat ();

		Assert.Pass ();
    }
}
