﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewSpyPanel : MonoBehaviour {

	public int spyLevel;
	public Text levelText;
	public Button newSpyButton;
	public Text descriptionText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		SpyingValues spyValues = FindObjectOfType<SpyingValues> ();

		newSpyButton.interactable = IsThisLevelResearched();

		int spyValueIndex = spyLevel - 1;
		descriptionText.text = "The spying will take " + spyValues.turnsNeededPerLevel[spyValueIndex] +" turns.\n" +
			(spyValues.findArmyTypeChancePerLevel[spyValueIndex]*100) + "% chance of finding army types\n" + 
			(spyValues.findArmyNumberChancePerLevel[spyValueIndex]*100) + "% chance of finding army numbers";
	}

	private bool IsThisLevelResearched(){
		ResearchManager researchManager = FindObjectOfType<ResearchManager> ();
		bool researched = false;

		switch (spyLevel) {
			case 1:
				researched = researchManager.IsAlreadyResearched (TechnologyType.Spying1);
				break;
			case 2:
				researched =  researchManager.IsAlreadyResearched (TechnologyType.Spying2);
				break;
			case 3:
				researched =  researchManager.IsAlreadyResearched (TechnologyType.Spying3);
				break;
		}

		return researched;
	}
}
