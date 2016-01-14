using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BuildValues : MonoBehaviour {

	// Action Related Definitions (Editor modifiable)
	public BuildingType[] actionBuildingsList;
	public TechnologyType[] actionBuildingsRequiredTechnologies;
	public int[] actionGenerationPointsPerBuilding;
	public int[] actionGenerationPointsThresholds;

	// Military Related Definitions (Editor modifiable)
	public BuildingType[] militaryBuildingsList;
	public TechnologyType[] militaryBuildingsRequiredTechnologies;
	public int[] militaryPointsPerBuilding;

	public Dictionary<BuildingType, int> actionBuildingsGenerationPointsDictionary;
	public Dictionary<BuildingType, int> militaryBuildingsMilitaryPointsDictionary;

	// Use this for initialization
	void Start () {
		actionBuildingsGenerationPointsDictionary = new Dictionary<BuildingType, int>();
		// Fill the points corresponding to each building
		for (int i=0; i<actionBuildingsList.Length && i<actionGenerationPointsPerBuilding.Length; i++) {
			actionBuildingsGenerationPointsDictionary[actionBuildingsList[i]] = actionGenerationPointsPerBuilding[i];
		}

		Debug.Log ("BuildValues.Start(): Added " +  actionBuildingsGenerationPointsDictionary.Keys.Count + " values to the actionBuildingsGenerationPointsDictionary");

		militaryBuildingsMilitaryPointsDictionary = new Dictionary<BuildingType, int>();
		// Fill the points corresponding to each building
		for (int i=0; i<militaryBuildingsList.Length && i<militaryPointsPerBuilding.Length; i++) {
			militaryBuildingsMilitaryPointsDictionary[militaryBuildingsList[i]] = militaryPointsPerBuilding[i];
		}

		Debug.Log ("BuildValues.Start(): Added " +  militaryBuildingsMilitaryPointsDictionary.Keys.Count + " values to the militaryBuildingsMilitaryPointsDictionary");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsBuildingResearched(bool isMilitary, int buildingLevel){
		TechnologyType technologyRequired;

		if (isMilitary) {
			technologyRequired = militaryBuildingsRequiredTechnologies [buildingLevel];
		} else {
			technologyRequired = actionBuildingsRequiredTechnologies[buildingLevel];
		}

		return FindObjectOfType<ResearchManager> ().IsAlreadyResearched (technologyRequired);
	}

	// This is called from Unity Editor
	void OnValidate(){
		// Be sure that the arrays has the same length
		if(actionBuildingsList.Length != actionGenerationPointsPerBuilding.Length){
			int[] newActionGenerationArray = new int[actionBuildingsList.Length];
			for(int i=0; i<newActionGenerationArray.Length && i<actionGenerationPointsPerBuilding.Length; i++){
				newActionGenerationArray[i] = actionGenerationPointsPerBuilding[i];
			}

			actionGenerationPointsPerBuilding = newActionGenerationArray;
		}
	}
}
