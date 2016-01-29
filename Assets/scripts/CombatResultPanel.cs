using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatResultPanel : MonoBehaviour {

	public Image[] republicanLossesImages;
	public Text[] republicanLossesTexts;
	public Image[] naziLossesImages;
	public Text[] naziLossesTexts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowPanel(Dictionary<ArmyType, int> republicanLosses, Dictionary<ArmyType, int> naziLosses){
		int counter = 0;

		// First hide all units
		for(int i=0; i<republicanLossesImages.Length && i<republicanLossesTexts.Length && 
			i<naziLossesImages.Length && i<naziLossesTexts.Length; i++){
			republicanLossesImages [i].gameObject.SetActive (false);
			republicanLossesTexts [i].gameObject.SetActive (false);
			naziLossesImages [i].gameObject.SetActive (false);
			naziLossesTexts [i].gameObject.SetActive (false);
		}

		// Then show the losses of each party
		foreach(KeyValuePair<ArmyType, int> army in republicanLosses){
			republicanLossesImages [counter].gameObject.SetActive (true);
			republicanLossesTexts [counter].gameObject.SetActive (true);
			republicanLossesImages [counter].sprite = FindObjectOfType<ArmyValues> ().GetArmy (army.Key).sprite;
			republicanLossesTexts [counter].text = "X" + army.Value;
			counter++;
		}

		counter = 0;

		foreach(KeyValuePair<ArmyType, int> army in naziLosses){
			naziLossesImages [counter].gameObject.SetActive (true);
			naziLossesTexts [counter].gameObject.SetActive (true);
			naziLossesImages [counter].sprite = FindObjectOfType<ArmyValues> ().GetArmy (army.Key).sprite;
			naziLossesTexts [counter].text = "X" + army.Value;
			counter++;
		}

		gameObject.SetActive (true);
	}
}
