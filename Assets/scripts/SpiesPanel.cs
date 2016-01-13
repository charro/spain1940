using UnityEngine;
using System.Collections;

public class SpiesPanel : MonoBehaviour {

	public GameObject[] spyPanelsList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshSpiesInfo(){
		Spy[] activeSpies = FindObjectOfType<SpyManager> ().GetActiveSpies ();

		bool alreadyShownAtLeastNewSpyButton = false;

		for(int spyLevel=0; spyLevel<spyPanelsList.Length && spyLevel<activeSpies.Length; spyLevel++){
			SpyPanel spyPanel = spyPanelsList [spyLevel].GetComponent<SpyPanel> ();
			spyPanel.SetSpy(activeSpies[spyLevel]);

			// If it's not researched yet, disable all spyButtons
			ResearchManager researchManager = FindObjectOfType<ResearchManager> ();
			if (!researchManager.IsAlreadyResearched (TechnologyType.Spying1)) {
				spyPanel.DisableNewSpyButton ();
			}
			else {
				// Be sure to only show the first panel with a null Spy
				if(activeSpies[spyLevel] == null){
					if(alreadyShownAtLeastNewSpyButton){
						spyPanelsList[spyLevel].SetActive(false);
					}
					else{
						spyPanelsList[spyLevel].SetActive(true);
						alreadyShownAtLeastNewSpyButton = true;
					}
				}
				else{
					spyPanelsList[spyLevel].SetActive(true);
				}
			}

		}

	}
}
