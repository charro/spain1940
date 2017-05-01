using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechnologyValues : MonoBehaviour {

	private Dictionary<TechnologyType, Technology> technologiesDictionary;

	// Use this for initialization
	void Awake () {
		// Gets all Technology children and adds them to the HashMap
		technologiesDictionary = new Dictionary<TechnologyType, Technology> ();

		foreach(Technology technology in GetComponentsInChildren<Technology>()){
			technologiesDictionary[technology.technologyType] = technology;
			Debug.Log("TECHNOLOGY ADDED TO TECHNOLOGY DICTIONARY: " + technology);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Technology GetTechnology(TechnologyType type){
		return technologiesDictionary[type];
	}


	public void RestoreDataFromSaveGame(SaveGameManager.SaveGameData gameData){
		foreach(SaveGameManager.SavedTechnology savedTechnology in gameData.savedTechnologies){
			Technology technology = technologiesDictionary[savedTechnology.type];

			technology.alreadyResearched = savedTechnology.alreadyResearched;
		}
	}

	public void FillSaveGameData(SaveGameManager.SaveGameData gameData){
		gameData.savedTechnologies.Clear ();

		foreach(Technology technology in technologiesDictionary.Values){
			gameData.savedTechnologies.Add (new SaveGameManager.SavedTechnology(technology));
		}
	}
}
