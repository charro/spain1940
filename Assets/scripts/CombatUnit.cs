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
		Refresh ();
	}

	public ArmyType GetArmyType(){
		return associatedArmySlot.armyType;
	}

	public bool isEmpty(){
		return associatedArmySlot.armyType == ArmyType.Empty;
	}

	void Awake () {
		// lastTimeCheckedUnits = associatedArmySlot.
		Refresh();
	}

	void Refresh (){
		if (associatedArmySlot == null || associatedArmySlot.armyType == ArmyType.Empty ||
			associatedArmySlot.armyAmount <= 0) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
			image.sprite = 
				FindObjectOfType<ArmyValues>().GetArmy(associatedArmySlot.armyType).sprite;
			units.text = 
				associatedArmySlot.armyAmount + "";
		}
	}

	// Update is called once per frame
	void Update () {
		// Update the units number
		units.text = 
			associatedArmySlot.armyAmount + "";
		
		// Check if all these units have been destroyed since last update
		if(lastTimeCheckedUnits > associatedArmySlot.armyAmount && associatedArmySlot.armyAmount==0){
			this.gameObject.SetActive (false);
			FindObjectOfType<CombatScreen> ().ShowExplosion (transform.position.x, transform.position.y);
			lastTimeCheckedUnits = associatedArmySlot.armyAmount;
		}
	}
}
