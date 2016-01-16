using UnityEngine;
using System.Collections;

public class CombatUnit : MonoBehaviour {

	public SpriteRenderer image;
	public TextMesh units;

	private RegionArmySlot associatedArmySlot;

	// private int lastTimeCheckedUnits;

	public void SetAssociatedArmySlot(RegionArmySlot armySlot){
		associatedArmySlot = armySlot;
	}

	void Awake () {
		// lastTimeCheckedUnits = associatedArmySlot.
		if (associatedArmySlot.armyType == ArmyType.Empty) {
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
		units.text = 
			associatedArmySlot.armyAmount + "";
	}
}
