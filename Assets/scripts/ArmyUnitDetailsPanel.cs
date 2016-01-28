using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmyUnitDetailsPanel : MonoBehaviour {

	public Text priceText;
	public Text attackText;
	public Text defenseText;
	public Text speedText;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void ShowArmyDetailsOfSlot(int armySlot){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		ArmyType armyType = gameManager.GetSelectedRegion().GetArmySlots()[armySlot].armyType;
		Army army = FindObjectOfType<ArmyValues> ().GetArmy (armyType);

		priceText.text = "" + army.price;
		attackText.text = "" + army.attack;
		defenseText.text = "" + army.defense;
		speedText.text = "" + army.speed;
	}
}
