using UnityEngine;
using System.Collections;

public class CombatScreen : MonoBehaviour {

	public CombatUnit[] republicanCombatUnits;
	public CombatUnit[] naziCombatUnits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCombatRegions(Region republican, Region nazi){
		foreach(CombatUnit combatUnit in republicanCombatUnits){
			combatUnit.gameObject.SetActive (false);
		}
		foreach(CombatUnit combatUnit in naziCombatUnits){
			combatUnit.gameObject.SetActive (false);
		}

		for (int i = 0; i < republican.armySlots.Length && i < republicanCombatUnits.Length; i++) {
			republicanCombatUnits [i].SetAssociatedArmySlot (republican.armySlots[i]);
			republicanCombatUnits [i].gameObject.SetActive (true);
		}

		for (int i = 0; i < nazi.armySlots.Length && i < naziCombatUnits.Length; i++) {
			naziCombatUnits [i].SetAssociatedArmySlot (nazi.armySlots[i]);
			naziCombatUnits [i].gameObject.SetActive (true);
		}
	}
}
