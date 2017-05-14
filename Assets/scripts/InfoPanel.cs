using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public Text regionNameText;
	public Image regionImage;
	public Sprite unknownArmySprite;
	public GameObject[] unitSlots;

	public Text regionMilitaryProduction;
	public Text totalMilitaryProduction;
	public Text regionActionProduction;
	public Text totalActionProduction;

	public GameObject infoPanel;
	public GameObject mainActionsPanel;
	public GameObject mainEnemyActionsPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close() {
		infoPanel.SetActive (false);

		if (FindObjectOfType<GameManager> ().GetSelectedRegion ().isNazi) {
			mainEnemyActionsPanel.SetActive (true);
		} else {
			mainActionsPanel.SetActive (true);
		}
	}

	public void UpdatePanelValues(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		Region selectedRegion = gameManager.GetSelectedRegion ();

		if (selectedRegion == null) {
			Debug.LogError ("InfoPanel: Update called, but no region selected !!");
		} else {

			// Update Region name and icon
			regionNameText.text = selectedRegion.name;
			regionImage.sprite = selectedRegion.GetCurrentSprite();

			// Spanish army owned region. Show actual values
			if(!selectedRegion.isNazi){
				RegionArmySlot[] armySlots = selectedRegion.GetArmySlots();

				for(int i=0; i<armySlots.Length && i<unitSlots.Length; i++){
					GameObject slot = unitSlots [i];

					if(armySlots[i].armyType == ArmyType.Empty){
						slot.SetActive(false);
					}
					else{
						slot.SetActive(true);
						slot.GetComponentInChildren<Text>().text = " X " + armySlots[i].armyAmount;
						slot.GetComponentInChildren<Image>().sprite = 
							FindObjectOfType<ArmyValues>().GetArmy(armySlots[i].armyType).sprite;
						slot.GetComponentInChildren<Button> ().interactable = true;
					}
				}

				// Production Info
				regionActionProduction.text = "" + selectedRegion.GetActionGenerationPoints();
				totalActionProduction.text = "" + economyManager.GetTotalActionGenerationPoints();

				// Military Info
				regionMilitaryProduction.text = "" + selectedRegion.GetMilitaryPointsGeneration ();
				totalMilitaryProduction.text = "" + economyManager.GetTotalMilitaryGenerationPoints();
			}
			// For Nazi owned, only show spied values or Unknown if no spied values available
			else{
				for(int i=0; i<unitSlots.Length; i++){
					GameObject slot = unitSlots [i];

					slot.SetActive(true);
					slot.GetComponentInChildren<Image>().sprite = unknownArmySprite;
					slot.GetComponentInChildren<Text>().text = "X ??";
					slot.GetComponentInChildren<Button> ().interactable = false;

					// Show last spied info in case it exists
					SpiedRegionInfo spiedInfo = selectedRegion.GetLastSpiedRegionInfo();
					if(spiedInfo != null){
						bool thereIsSpyInfoForThisRegion = false;

						if(spiedInfo.spiedArmyTypes.Length >= i && spiedInfo.spiedArmyTypes[i] != ArmyType.Unknown &&
						   spiedInfo.spiedArmyTypes[i] != ArmyType.Empty){
							slot.GetComponentInChildren<Image>().sprite = 
								FindObjectOfType<ArmyValues>().GetArmy(spiedInfo.spiedArmyTypes[i]).sprite;
							thereIsSpyInfoForThisRegion = true;
						}
						if(spiedInfo.spiedArmyAmounts.Length >= i && spiedInfo.spiedArmyAmounts[i] > 0){
							slot.GetComponentInChildren<Text>().text = " X " + spiedInfo.spiedArmyAmounts[i];
							thereIsSpyInfoForThisRegion = true;
						}

						if(thereIsSpyInfoForThisRegion){
							slot.GetComponentInChildren<Button> ().interactable = true;
						}
					}

				}

				regionActionProduction.text = "??";
				totalActionProduction.text = "??";

				// Military Info
				regionMilitaryProduction.text = "??";
				totalMilitaryProduction.text = "??";
			}
		}
	}

}
