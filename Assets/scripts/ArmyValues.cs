using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ArmyValues : MonoBehaviour {

	public ArmyType[] armyUnitList;
	public Sprite[] armySpriteList;
	public int[] armyUnitPrices;

//	public AirForceType[] airForceUnits;
//	public Sprite[] airForceSprites;

	public Dictionary<ArmyType, Sprite> armySpritesDictionary;
	public Dictionary<ArmyType, int> armyPricesDictionary;
//	public Dictionary<AirForceType, Sprite> airForceSprites;

	// Use this for initialization
	void Start () {

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This is called from Unity Editor
	void OnValidate(){
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
	}
}
