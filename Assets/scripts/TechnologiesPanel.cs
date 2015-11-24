﻿using UnityEngine;
using System.Collections;

public class TechnologiesPanel : MonoBehaviour {

	public GameObject technologyDetailPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		technologyDetailPanel.SetActive (false);
	}

	public void Hide(){
		gameObject.SetActive (false);
	}

	public void ShowTechnologyDetails(Technology technology){
		technologyDetailPanel.GetComponent<TechnologyDetailPanel> ().OpenDetailResearchPanel (technology);
	}
}
