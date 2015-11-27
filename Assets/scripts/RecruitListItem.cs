using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitListItem : MonoBehaviour {

	public Text priceText;
	public ArmyType armyType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		int armyPrice = FindObjectOfType<ArmyValues>().armyPricesDictionary[armyType]; 
		priceText.text = "" + armyPrice;
	}

}
