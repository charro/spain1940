using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject mainActionsPanel;
	public GameObject mainEnemyActionsPanel;
	public GameObject fightPanel;
	public GameObject midBackground;

	private static UIManager singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void showMainActions(){
		singleton.mainActionsPanel.SetActive (true);
	}

	public static void showMainEnemyActions(){
		singleton.mainEnemyActionsPanel.SetActive (true);
	}

	public static void hideMainActionPanels(){
		singleton.mainActionsPanel.SetActive (false);
		singleton.mainEnemyActionsPanel.SetActive (false);
	}

	public static void showMidBackground(bool show){
		singleton.midBackground.SetActive (show);
	}

	public static void regionSelected(){
		showMidBackground(true);

		// Disable here input for rest of regions
	}

	public static void regionUnselected(){
		showMidBackground(false);

		// Enable here input again for rest of regions
	}
}