using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject mainActionsPanel;
	public GameObject fightPanel;

	private static GUIManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void showMainActions(bool show){
		instance.mainActionsPanel.SetActive (show);

		// If showing Main actions, hide the rest
		if(show){
			instance.fightPanel.SetActive(false);
		}
	}
}
