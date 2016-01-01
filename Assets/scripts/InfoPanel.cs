using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public Text regionNameText;
	public Sprite unknownArmySprite;
	public GameObject[] unitSlots;

	public Text regionMilitaryProduction;
	public Text totalMilitaryProduction;
	public Text regionActionProduction;
	public Text totalActionProduction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePanelValues(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		Region selectedRegion = gameManager.GetSelectedRegion ();

		if (selectedRegion == null) {
			Debug.LogError ("InfoPanel: Update called, but no region selected !!");
		} else {

			// Spanish army owned region. Show actual values
			if(!selectedRegion.isNazi){
				RegionArmySlot[] armySlots = selectedRegion.GetArmySlots();

				for(int i=0; i<armySlots.Length && i<unitSlots.Length; i++){
					if(armySlots[i].armyType == ArmyType.Empty){
						unitSlots[i].SetActive(false);
					}
					else{
						unitSlots[i].SetActive(true);
						unitSlots[i].GetComponentInChildren<Text>().text = " X " + armySlots[i].armyAmount;
						unitSlots[i].GetComponentInChildren<Image>().sprite = 
							FindObjectOfType<ArmyValues>().armySpritesDictionary[armySlots[i].armyType];
					}
				}

				// Production Info
				regionActionProduction.text = "" + selectedRegion.GetActionGenerationPoints();
				totalActionProduction.text = "" + economyManager.GetTotalActionGenerationPoints();
			}
			// For Nazi owned, only show spied values or Unknown if no spied values available
			else{
				for(int i=0; i<unitSlots.Length; i++){
					unitSlots[i].SetActive(true);
					unitSlots[i].GetComponentInChildren<Image>().sprite = unknownArmySprite;
					unitSlots[i].GetComponentInChildren<Text>().text = "X ??";

					// Show last spied info in case it exists
					SpiedRegionInfo spiedInfo = selectedRegion.GetLastSpiedRegionInfo();
					if(spiedInfo != null){
						if(spiedInfo.spiedArmyTypes.Length >= i && spiedInfo.spiedArmyTypes[i] != ArmyType.Unknown &&
						   spiedInfo.spiedArmyTypes[i] != ArmyType.Empty){
							unitSlots[i].GetComponentInChildren<Image>().sprite = 
								FindObjectOfType<ArmyValues>().armySpritesDictionary[spiedInfo.spiedArmyTypes[i]];
						}
						if(spiedInfo.spiedArmyAmounts.Length >= i && spiedInfo.spiedArmyAmounts[i] > 0){
							unitSlots[i].GetComponentInChildren<Text>().text = " X " + spiedInfo.spiedArmyAmounts[i];
						}
					}

				}
			}
		}
	}

}
