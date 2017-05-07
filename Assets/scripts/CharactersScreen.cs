using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersScreen : MonoBehaviour {

	void OnEnable(){
		UIManager.hideAllPanels ();
	}

	void OnDisable(){
		UIManager.ShowHUDPanel ();
	}
}
