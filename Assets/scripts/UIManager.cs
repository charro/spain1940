using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject HUDPanel;
	//public GameObject basicPanel;
	public GameObject mainActionsPanel;
	public GameObject mainEnemyActionsPanel;
	public GameObject fightPanel;
	public GameObject infoPanel;
	public GameObject recruitPanel;
	public GameObject buildPanel;
	public GameObject midBackground;
	public GameObject midBackgroundEnemy;
	public GameObject moveTroopsPanel;
	public GameObject messagesPanel;
	public GameObject loadingScreen;
	public GameObject popUpPanel;
	public GameObject researchSpyPanel;
	public GameObject technologiesPanel;

	public Material defaultSpriteMaterial;
	public Material disabledSpriteMaterial;

	private static UIManager singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void onRegionSelected(Region newRegionSelected){
		UIManager.showMidBackground (true, newRegionSelected.isNazi);
		UIManager.ShowPanelsWhenRegionSelected(newRegionSelected.isNazi);
		if (newRegionSelected.isNazi) {
			GameObject.Find ("/GUI/MainEnemyActionsPanel/RegionNameText").GetComponent<Text> ().text = 
				newRegionSelected.name;
		} else {
			GameObject.Find ("/GUI/MainActionsPanel/RegionNameText").GetComponent<Text> ().text = 
				newRegionSelected.name;
		}

	}

	// De-select the current region and go back to Spain map
	public static void onRegionUnselected(Region previouslySelectedRegion){
		UIManager.showMidBackground (false, previouslySelectedRegion.isNazi);
		UIManager.HidePanelsWhenRegionUnselected();
		UIManager.ShowHUDPanel ();
	}

	public static void showMainActions(bool isNazi){

		if(isNazi){
			singleton.mainEnemyActionsPanel.SetActive (true);
		}
		else{
			singleton.mainActionsPanel.SetActive (true);
		}

	}

	public static void showMidBackground(bool show, bool isNazi){
		if(isNazi){
			singleton.midBackgroundEnemy.SetActive (show);
		}
		else{
			singleton.midBackground.SetActive (show);
		}
	}

	public void ShowInfoPanel(){
		hideAllPanels ();
		infoPanel.SetActive(true);
		
		InfoPanel info = infoPanel.GetComponent<InfoPanel>();
		info.UpdatePanelValues ();
	}

	public static void ShowMessagesPanel(){
		singleton.messagesPanel.SetActive (true);
	}

	public static void ShowMoveTroopsPanel(){
		singleton.moveTroopsPanel.SetActive (true);
	}

	public static void ShowHUDPanel(){
		singleton.HUDPanel.SetActive (true);
		singleton.HUDPanel.GetComponent<HUD> ().Refresh ();
	}

	public static void ShowResearchSpyPanel(){
		singleton.researchSpyPanel.SetActive (true);
		singleton.HUDPanel.SetActive (false);
	}

	public static void ShowPanelsWhenRegionSelected(bool isNazi){
		hideAllPanels ();
		showMainActions (isNazi);
		// Disable here input for rest of regions
	}

	public static void HidePanelsWhenRegionUnselected(){
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
		singleton.recruitPanel.SetActive(false);
		singleton.buildPanel.SetActive(false);
		singleton.moveTroopsPanel.SetActive(false);
		singleton.HUDPanel.SetActive (false);
		singleton.messagesPanel.SetActive (false);
		singleton.loadingScreen.SetActive (false);
		singleton.researchSpyPanel.SetActive (false);
		singleton.technologiesPanel.SetActive (false);
	}

	public static bool IsMainActionsShown(){
		return 	singleton.mainActionsPanel.activeInHierarchy ||
			singleton.mainEnemyActionsPanel.activeInHierarchy;
	}

	public static bool IsAnyGUIPanelShownButHUDPanel(){
		return IsMainActionsShown () ||
				singleton.fightPanel.activeInHierarchy ||
				singleton.infoPanel.activeInHierarchy ||
				singleton.recruitPanel.activeInHierarchy ||
				singleton.buildPanel.activeInHierarchy ||
				singleton.moveTroopsPanel.activeInHierarchy ||
				singleton.recruitPanel.activeInHierarchy ||
				singleton.researchSpyPanel.activeInHierarchy;
	}

	public static bool IsAnyGUIPanelShown(){
		return IsAnyGUIPanelShownButHUDPanel () ||
				singleton.HUDPanel.activeInHierarchy;;
	}

	public static Material GetDefaultMaterial(){
		return singleton.defaultSpriteMaterial;
	}

	public static Material GetDisabledMaterial(){
		return singleton.disabledSpriteMaterial;
	}

	public static void RefreshHUDPanel(){
		singleton.HUDPanel.GetComponent<HUD>().Refresh();
	}

	public static void ShowLoadingScreen(){
		singleton.loadingScreen.SetActive (true);
	}

	public static void HideLoadingScreen(){
		singleton.loadingScreen.SetActive (false);
	}

	public void ShowPopUp(PopUpType type){
		popUpPanel.GetComponent<PopUp> ().NewPopUp (type);
		popUpPanel.SetActive (true);
	}

	public void HidePopUp(){
		popUpPanel.SetActive (false);
	}

	public static void ShowLoadingTmp(){
		singleton.StartCoroutine (ShowLoadingScreenForMilisecs(2f));
	}

	static IEnumerator ShowLoadingScreenForMilisecs(float secs){
		ShowLoadingScreen ();
		yield return new WaitForSeconds (secs);
		HideLoadingScreen ();
		ShowHUDPanel ();
	}
}