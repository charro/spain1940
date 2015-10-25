using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildValues : MonoBehaviour {

	// Action Related
	public BuildingType[] actionBuildingsList;
	public int[] actionGenerationPointsPerBuilding;
	public int[] actionGenerationPointsThresholds;

	// Military Related
	// TODO

	public Dictionary<BuildingType, int> actionBuildingsGenerationPointsDictionary;

	// Use this for initialization
	void Start () {
		actionBuildingsGenerationPointsDictionary = new Dictionary<BuildingType, int>();
		// Fill the army sprites list
		for (int i=0; i<actionBuildingsList.Length && i<actionGenerationPointsPerBuilding.Length; i++) {
			actionBuildingsGenerationPointsDictionary.Add(actionBuildingsList[i], actionGenerationPointsPerBuilding[i]);
		}

		Debug.Log ("BuildValues.Start(): Added " +  actionBuildingsGenerationPointsDictionary.Keys.Count + " values to the actionBuildingsGenerationPointsDictionary");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
