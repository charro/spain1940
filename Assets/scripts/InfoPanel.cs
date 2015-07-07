using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour {

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
			ArmyType[] armySlotTypes = selectedRegion.ArmySlotsTypes();
			int[] armySlotUnits = selectedRegion.ArmySlotsUnits();

			for(int i=0; i<armySlotTypes.Length && i<unitSlots.Length && i<armySlotUnits.Length; i++){
				if(armySlotTypes[i] == ArmyType.Empty){
					unitSlots[i].SetActive(false);
				}
				else{
					unitSlots[i].SetActive(true);
					unitSlots[i].GetComponentInChildren<Text>().text = " X " + armySlotUnits[i];
					// TODO: SET SPRITE HERE
					// unitSlots[i].GetComponent<Image>().sprite = armySlotUnits[i];
				}
			}
		}
	}
}
