using UnityEngine;
using System.Collections;

public class CombatScreen : MonoBehaviour {

	public CombatUnit[] republicanCombatUnits;
	public CombatUnit[] naziCombatUnits;
	public GameObject missMessage;
	public ParticleSystem unitExplosion;
	//public Transform explosion;
	public GameObject unitShootingParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowShooting(bool isNazi, ArmyType armyType){
		GameObject shootingUnit = GetUnitByType (isNazi, armyType);
		Transform transform = shootingUnit.transform;

		// Trigger unit shooting animation
		shootingUnit.GetComponentInChildren<Animator> ().Play ("shooting");

		// Show shooting particles
		int offset = isNazi ? -1 : 1;
		Quaternion rotation = isNazi ? Quaternion.Euler(new Vector3(0, 180, 0)) : transform.rotation;
		Vector3 shotPosition = 
			new Vector3 (transform.position.x + offset, transform.position.y, transform.position.z);
		Instantiate(unitShootingParticles, shotPosition, rotation);
	}

	public void ShowExplosion(float x, float y){
		Vector3 explosionPosition = new Vector3 (x, y, unitExplosion.transform.position.z);
		Instantiate(unitExplosion, explosionPosition, transform.rotation);
	}

	public void SetCombatRegions(Region republican, Region nazi){
		RegionArmySlot emptySlot = new RegionArmySlot ();

		foreach(CombatUnit combatUnit in republicanCombatUnits){
			combatUnit.gameObject.SetActive (false);
			combatUnit.SetAssociatedArmySlot (emptySlot);
		}
		foreach(CombatUnit combatUnit in naziCombatUnits){
			combatUnit.gameObject.SetActive (false);
			combatUnit.SetAssociatedArmySlot (emptySlot);
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

	public void ShowMissedUnitMessage(bool isNazi, ArmyType armyType, float seconds){
		GameObject missedUnit = GetUnitByType (isNazi, armyType);
		missMessage.transform.position = new Vector3 (missedUnit.transform.position.x + 2,
													  missedUnit.transform.position.y, 
													  missMessage.transform.position.z);
		StartCoroutine(ShowObjectForSecs (missMessage, seconds));
	}

	/*********************  PRIVATE METHODS *****************************************/

	private IEnumerator ShowObjectForSecs(GameObject gameObject, float secs){
		gameObject.SetActive (true);
		yield return new WaitForSeconds (secs);
		gameObject.SetActive (false);
	}

	private GameObject GetUnitByType(bool isNazi, ArmyType armyType){
		CombatUnit[] combatUnitsAttacked = (isNazi ? naziCombatUnits : republicanCombatUnits);
		GameObject missedUnit = combatUnitsAttacked[0].gameObject;
		foreach(CombatUnit combatUnit in combatUnitsAttacked){
			if(!combatUnit.isEmpty() && combatUnit.GetArmyType() == armyType){
				missedUnit = combatUnit.gameObject;
				break;
			}
		}

		return missedUnit;
	}
}
