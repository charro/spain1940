using UnityEngine;
using System.Collections;

public class CombatUnit : MonoBehaviour {

	public SpriteRenderer image;
	public TextMesh units;

	private RegionArmySlot associatedArmySlot;

	private int lastTimeCheckedUnits;

	public void SetAssociatedArmySlot(RegionArmySlot armySlot){
		associatedArmySlot = armySlot;
		lastTimeCheckedUnits = associatedArmySlot.armyAmount;
	}

	public ArmyType GetArmyType(){
		return associatedArmySlot.armyType;
	}

	public bool isEmpty(){
		return associatedArmySlot.armyType == ArmyType.Empty;
	}

	void Awake () {
		// lastTimeCheckedUnits = associatedArmySlot.
		if (associatedArmySlot == null || associatedArmySlot.armyType == ArmyType.Empty) {
			gameObject.SetActive (false);
		} else {
			image.sprite = 
				FindObjectOfType<ArmyValues>().GetArmy(associatedArmySlot.armyType).sprite;
			units.text = 
				associatedArmySlot.armyAmount + "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Check if this any of this units has been destroyed since last update
		if(lastTimeCheckedUnits > associatedArmySlot.armyAmount){
			units.text = 
				associatedArmySlot.armyAmount + "";
			FindObjectOfType<CombatScreen> ().ShowExplosion (transform.position.x, transform.position.y);
			lastTimeCheckedUnits = associatedArmySlot.armyAmount;
		}
	}
}
