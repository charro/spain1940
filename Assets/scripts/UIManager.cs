using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject mainActionsPanel;
	public GameObject mainEnemyActionsPanel;
	public GameObject fightPanel;
	public GameObject infoPanel;
	public GameObject recruitPanel;
	public GameObject midBackground;

	private static UIManager singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void showMainActions(bool isNazi){
		hideAllPanels ();

		if(isNazi){
			singleton.mainEnemyActionsPanel.SetActive (true);
		}
		else{
			singleton.mainActionsPanel.SetActive (true);
		}

	}

	public static void showMidBackground(bool show){
		singleton.midBackground.SetActive (show);
	}

	public void ShowInfoPanel(){
		hideAllPanels ();
		infoPanel.SetActive(true);
		
		InfoPanel info = infoPanel.GetComponent<InfoPanel>();
		info.UpdatePanelValues ();
	}

	public static void ShowUIPanelsOnRegionSelected(bool isNazi){
		showMidBackground(true);
		hideAllPanels ();
		showMainActions (isNazi);
		// Disable here input for rest of regions
	}

	public static void HidePanelsOnRegionUnselected(){
		showMidBackground(false);
		hideAllPanels ();
		// Enable here input again for rest of regions
	}

	/*
	public static void RefreshInfoPanel(){
		singleton.infoPanel.GetComponent<InfoPanel>().UpdatePanelValues ();
	}*/

	public static void hideAllPanels(){
		singleton.mainActionsPanel.SetActive(false);
		singleton.mainEnemyActionsPanel.SetActive(false);
		singleton.fightPanel.SetActive(false);
		singleton.infoPanel.SetActive(false);
		singleton.midBackground.SetActive(false);
	}

	public static bool IsMainActionsShown(){
		return 	singleton.mainActionsPanel.activeInHierarchy ||
			singleton.mainEnemyActionsPanel.activeInHierarchy;
	}

	public static bool IsAnyGUIPanelShown(){
		return IsMainActionsShown () ||
				singleton.fightPanel.activeInHierarchy ||
				singleton.infoPanel.activeInHierarchy ||
				singleton.recruitPanel.activeInHierarchy;
	}

}