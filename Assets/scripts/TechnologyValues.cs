using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechnologyValues : MonoBehaviour {

	private Dictionary<TechnologyType, Technology> technologiesDictionary;

	// Use this for initialization
	void Start () {
		// Gets all Technology children and adds them to the HashMap
		technologiesDictionary = new Dictionary<TechnologyType, Technology> ();

		foreach(Technology technology in GetComponentsInChildren<Technology>()){
			technologiesDictionary.Add(technology.technologyType, technology);
			Debug.Log("TECHNOLOGY ADDED TO TECHNOLOGY DICTIONARY: " + technology);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Technology GetTechnology(TechnologyType type){
		return technologiesDictionary[type];
	}
}
