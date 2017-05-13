using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour {

	public string[] introStrings;
	public string[] winningStrings;
	public string[] losingStrings;
	public string[] dialogStrings;

	public GameObject franz;
	public GameObject francois;
	public GameObject paquito;
	public SkinnedMeshRenderer curtain;

	public GameObject charactersContainer;
	public GameObject dialogsContainer;
	public GameObject leftDialog;
	public Text leftText;
	public GameObject middleDialog;
	public Text middleText;
	public GameObject rightDialog;
	public Text rightText;

	public Material naziMaterial;
	public Material spainMaterial;

	public GameObject resultContainer;
	public Text resultTitle;

	private int currentDialogIndex;

	private bool dialogsEnabled;

	private string[] currentStrings;

	public void StartWinningDialog(){
		StartDialog (DialogType.winDialog);
	}
		
	public void StartLosingDialog(){
		StartDialog (DialogType.loseDialog);
	}

	public void StartDialog(int dialogNumber){
		StartDialog ((DialogType)dialogNumber);
	}

	public void StartDialog(DialogType dialogNumber){

		switch(dialogNumber){
		case DialogType.introDialog:
			currentStrings = introStrings;
			break;
		case DialogType.winDialog:
			currentStrings = winningStrings;
			break;
		case DialogType.loseDialog:
			currentStrings = losingStrings;
			break;
		case DialogType.firstRegionDialog:
			currentStrings = dialogStrings;
			break;
		}

		dialogsEnabled = enabled;
		currentDialogIndex = 0;
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.DialogState);
	}

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "intro") {
			currentStrings = introStrings;
		}
	}

	// Update is called once per frame
	void Update () {

		// INTRO SCENE DIALOG
		if (SceneManager.GetActiveScene ().name == "intro") {
			if (FindObjectOfType<LevelManager> ().storyFinished && Input.GetMouseButtonUp (0)) {
				if (StillDialog ()) {
					AdvanceDialog ();
				} else {
					// Show Nazi flag and let game ready to go to gameplay
					hideDialogs ();
					francois.gameObject.SetActive (true);
					franz.gameObject.SetActive (true);
					curtain.gameObject.SetActive (true);
					this.gameObject.SetActive (false);
				}
			}
		} 

		// MAIN SCENE DIALOG
		else {
			if (dialogsEnabled && Input.GetMouseButtonUp (0)) {
				if (StillDialog ()) {
					AdvanceDialog ();
				} else {
					EndDialog ();
				}
			}
		}

	}

	private bool StillDialog(){
		return currentDialogIndex < currentStrings.Length;
	}

	private void AdvanceDialog(){
		if(!charactersContainer.activeSelf){
			charactersContainer.SetActive (true);
		}

		string currentDialog = currentStrings[currentDialogIndex];

		if (currentDialog.StartsWith ("L_")) {
			currentDialog = currentDialog.Remove (0, 2);
			ShowLeftText (currentDialog);
		} else if (currentDialog.StartsWith ("R_")) {
			currentDialog = currentDialog.Remove (0, 2);
			ShowRightText (currentDialog);
		} else if (currentDialog.StartsWith ("M_")) {
			currentDialog = currentDialog.Remove (0, 2);
			ShowMiddleText (currentDialog);
		} else if (currentDialog == "END_GAME_NAZI") {
			hideAll ();
			showCurtainAndEndGame (false);
		} else if (currentDialog == "END_GAME_SPAIN") {
			hideAll ();
			showCurtainAndEndGame (true);
		}

		currentDialogIndex++;
	}

	private void ShowLeftText(string newtext){
		dialogsContainer.gameObject.SetActive (true);
		leftDialog.gameObject.SetActive (true);
		middleDialog.gameObject.SetActive (false);
		rightDialog.gameObject.SetActive (false);

		francois.gameObject.SetActive (true);
		paquito.gameObject.SetActive (false);
		franz.gameObject.SetActive (false);

		leftText.text = newtext;
	}

	private void ShowMiddleText(string newtext){
		dialogsContainer.gameObject.SetActive (true);
		leftDialog.gameObject.SetActive (false);
		middleDialog.gameObject.SetActive (true);
		rightDialog.gameObject.SetActive (false);

		francois.gameObject.SetActive (false);
		paquito.gameObject.SetActive (true);
		franz.gameObject.SetActive (false);

		middleText.text = newtext;
	}

	private void ShowRightText(string newtext){
		dialogsContainer.gameObject.SetActive (true);
		leftDialog.gameObject.SetActive (false);
		middleDialog.gameObject.SetActive (false);
		rightDialog.gameObject.SetActive (true);

		francois.gameObject.SetActive (false);
		paquito.gameObject.SetActive (false);
		franz.gameObject.SetActive (true);

		rightText.text = newtext;
	}

	private void hideDialogs(){
		dialogsContainer.gameObject.SetActive (false);
	}

	private void hideAll(){
		hideDialogs ();

		francois.gameObject.SetActive (false);
		paquito.gameObject.SetActive (false);
		franz.gameObject.SetActive (false);
	}

	private void showCurtainAndEndGame(bool win){
		dialogsEnabled = false;
		this.gameObject.SetActive (true);

		resultContainer.SetActive (true);
		curtain.material = win ? spainMaterial : naziMaterial;
		resultTitle.text = win ? "SI, SI, SI \nSPAIN IS NOW FREE !!!\n CONGRATULATIONS !!" : "NEIN NEIN NEIN !! \n THE THIRD REICH IS HERE !!";

		curtain.gameObject.SetActive (true);
		resultTitle.gameObject.SetActive (true);
	}

	private void EndDialog(){
		charactersContainer.SetActive (false);
		FindObjectOfType<GameStateMachine> ().SwitchToStateIdle ();
		dialogsEnabled = false;
	}
}
