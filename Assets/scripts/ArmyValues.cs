using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ArmyValues : MonoBehaviour {

	/*
	public ArmyType[] armyUnitList;
	public Sprite[] armySpriteList;
	public int[] armyUnitPrices;
	public int[] armyAttackValues;
	public int[] armyDefenseValues;
	public int[] armySpeedValues;*/

//	public AirForceType[] airForceUnits;
//	public Sprite[] airForceSprites;

	private Dictionary<ArmyType, Army> armiesDictionary;

	private List<Army> naziArmies = new List<Army> ();

//	public Dictionary<ArmyType, Sprite> armySpritesDictionary;
//	public Dictionary<ArmyType, int> armyPricesDictionary;
//	public Dictionary<AirForceType, Sprite> airForceSprites;

	public void SetArmyDictionary(Dictionary<ArmyType, Army> newArmiesDictionary){
		armiesDictionary = newArmiesDictionary;
	}

	public void SetNaziArmiesList(List<Army> newNaziArmies){
		naziArmies = newNaziArmies;
	}

	// Use this for initialization
	void Start () {
		// Gets all Technology children and adds them to the HashMap
		armiesDictionary = new Dictionary<ArmyType, Army> ();

		foreach(Army army in GetComponentsInChildren<Army>()){
			armiesDictionary[army.armyType] = army;
			if((int)army.armyType >= 100){
				naziArmies.Add (army);
			}
			Debug.Log("ARMY ADDED TO ARMIES DICTIONARY: " + army);
		}
		/*
		armySpritesDictionary = new Dictionary<ArmyType, Sprite>();
		// Fill the army sprites list
		for (int i=0; i<armyUnitList.Length && i<armySpriteList.Length; i++) {
			armySpritesDictionary.Add(armyUnitList[i], armySpriteList[i]);
		}

		Debug.Log ("ArmyValues.Start(): Added " +  armySpritesDictionary.Keys.Count + " sprites to the armySpritesDictionary");

		armyPricesDictionary = new Dictionary<ArmyType, int>();
		// Fill the army prices list
		for (int i=0; i<armyUnitPrices.Length && i<armyUnitPrices.Length; i++) {
			armyPricesDictionary.Add(armyUnitList[i], armyUnitPrices[i]);
		}

		Debug.Log ("ArmyValues.Start(): Added " +  armyPricesDictionary.Keys.Count + " prices to the armyPricesDictionary");
		*/


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Army GetArmy(ArmyType armyType){
		return armiesDictionary[armyType];
	}

	public List<Army> GetNaziArmies(){
		return naziArmies;
	}

	public static bool isNazi(ArmyType armyType){
		return armyType >= ArmyType.NaziJager;
	}

	// This is called from Unity Editor
/*	void OnValidate(){
		// Be sure that the arrays has the same length
		if(armyUnitList.Length != armySpriteList.Length){
			Sprite[] newSpriteList = new Sprite[armyUnitList.Length];
			for(int i=0; i<newSpriteList.Length && i<armySpriteList.Length; i++){
				newSpriteList[i] = armySpriteList[i];
			}

			armySpriteList = newSpriteList;
		}

		if(armyUnitList.Length != armyUnitPrices.Length){
			int[] newUnitPricesList = new int[armyUnitList.Length];
			for(int i=0; i<newUnitPricesList.Length && i<armyUnitPrices.Length; i++){
				newUnitPricesList[i] = armyUnitPrices[i];
			}
			
			armyUnitPrices = newUnitPricesList;
		}
	}*/
}
