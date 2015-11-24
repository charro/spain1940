﻿using UnityEngine;
using System.Collections;

public class ResearchManager : MonoBehaviour {

	private Technology currentResearchedTechnology = null;
	private Technology lastSelectedTechnology = null;
	private int turnsToEndResearching = 0;

	// Use this for initialization
	void Start () {
		EventManager.OnPassTurn += PassTurn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Technology GetCurrentResearchedTechnology(){
		return currentResearchedTechnology;
	}

	public void SetSelectedTechnology(Technology selectedTechnology){
		 lastSelectedTechnology = selectedTechnology;
	}

	public void ShowConfirmationPopUp(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmResearch);
	}

	public void StartResearchingTechnology(){
		currentResearchedTechnology =  lastSelectedTechnology;
		turnsToEndResearching = currentResearchedTechnology.turnsNeeded;
		FindObjectOfType<TechnologiesPanel> ().Hide ();
		FindObjectOfType<ResearchSpyPanel> ().Hide ();
		FindObjectOfType<HUD> ().Refresh ();
		FindObjectOfType<PopUpMessages> ().ShowDropDownMessageForSecs ("RESEARCH OF TECHNOLOGY " + currentResearchedTechnology + " STARTED", 5);
	}

	public bool IsAnyTechnologyBeingResearched(){
		return currentResearchedTechnology != null;
	}

	// This function is called automatically by EventManager when Player pass turn
	public void PassTurn(){
		Debug.Log ("RESEARCH MANAGER => PASS TURN.  Turns to finish: " + turnsToEndResearching);

		if(IsAnyTechnologyBeingResearched()){
			if(turnsToEndResearching > 0){
				turnsToEndResearching--;
				if(turnsToEndResearching == 0){
					ResearchFinished();
				}
			}
		}

	}

	private void ResearchFinished(){
		FindObjectOfType<PopUpMessages> ().ShowDropDownMessageForSecs ("TECHNOLOGY " + currentResearchedTechnology + " finished", 5);
		currentResearchedTechnology.alreadyResearched = true;
		currentResearchedTechnology = null;
	}

	public bool IsAlreadyResearched(TechnologyType technologyType){
		return FindObjectOfType<TechnologyValues> ().GetTechnology (technologyType).alreadyResearched;
	}
}
