using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public Sprite unknownArmySprite;
	public GameObject[] unitSlots;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePanelValues(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
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
			}
			// For Nazi owned, only show spied values
			else{
				for(int i=0; i<unitSlots.Length; i++){
					unitSlots[i].SetActive(true);
					unitSlots[i].GetComponentInChildren<Image>().sprite = unknownArmySprite;
					unitSlots[i].GetComponentInChildren<Text>().text = "unknown";
				}
			}
		}
	}
}
