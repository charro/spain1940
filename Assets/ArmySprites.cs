using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmySprites : MonoBehaviour {

	public ArmyType[] armyUnitList;
	public Sprite[] armySpriteList;

//	public AirForceType[] airForceUnits;
//	public Sprite[] airForceSprites;

	public Dictionary<ArmyType, Sprite> armySpritesDictionary;
//	public Dictionary<AirForceType, Sprite> airForceSprites;

	// Use this for initialization
	void Start () {

		armySpritesDictionary = new Dictionary<ArmyType, Sprite>();
		// Fill the army sprites list
		for (int i=0; i<armyUnitList.Length && i<armySpriteList.Length; i++) {
			armySpritesDictionary.Add(armyUnitList[i], armySpriteList[i]);
		}

		Debug.Log ("ArmySprites.Start(): Added " +  armySpritesDictionary.Keys.Count + " sprites to the armySpritesDictionary");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
