using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmyUnitDetailsPanel : MonoBehaviour {

	public Text unitName;
	public Image unitImage;

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

		ResearchManager researchManager = FindObjectOfType<ResearchManager> ();
		int additionalAttack = researchManager.GetAdditionalAttackForArmy (armyType);
		int additionalDefense = researchManager.GetAdditionalDefenseForArmyAndRegion (armyType, gameManager.GetSelectedRegion());
		int additionalSpeed = researchManager.GetAdditionalSpeedForArmy (armyType);

		unitName.text = "" + army.armyDescription;
		unitImage.sprite = army.sprite;

		priceText.text = "" + army.price;
		attackText.text = army.attack + (additionalAttack > 0 ? " (+" + additionalAttack + ")" : "");
		defenseText.text = "" + army.defense + (additionalDefense > 0 ? " (+" + additionalDefense + ")" : "");
		speedText.text = "" + army.speed + (additionalSpeed > 0 ? " (+" + additionalSpeed + ")" : "");
	}
}
