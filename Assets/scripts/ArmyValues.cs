using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
}
