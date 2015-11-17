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
			spyPanelsList[i].GetComponent<SpyPanel>().SetSpy(activeSpies[i]);

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
