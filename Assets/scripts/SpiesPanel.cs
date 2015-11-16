using UnityEngine;
using System.Collections;

public class SpiesPanel : MonoBehaviour {

	public SpyPanel[] spiesList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshSpiesInfo(){
		Spy[] activeSpies = FindObjectOfType<SpyManager> ().GetActiveSpies ();

		for(int i=0; i<spiesList.Length && i<activeSpies.Length; i++){
			spiesList[i].SetSpy(activeSpies[i]);
		}
	}
}
