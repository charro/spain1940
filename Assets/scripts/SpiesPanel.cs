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

		for(int i=0; i<spyPanelsList.Length && i<activeSpies.Length; i++){
			SpyPanel spyPanel = spyPanelsList [i].GetComponent<SpyPanel> ();
			spyPanel.SetSpy(activeSpies[i]);

			// If it's not researched yet, disable all spyBUttons
			if (!FindObjectOfType<ResearchManager> ().IsAlreadyResearched (TechnologyType.Spying)) {
				spyPanel.DisableNewSpyButton ();
			} 
			else {
				// Be sure to only show the first panel with a null Spy
				if(activeSpies[i] == null){
					if(alreadyShownAtLeastNewSpyButton){
						spyPanelsList[i].SetActive(false);
					}
					else{
						spyPanelsList[i].SetActive(true);
						alreadyShownAtLeastNewSpyButton = true;
					}
				}
				else{
					spyPanelsList[i].SetActive(true);
				}
			}

		}

	}
}
