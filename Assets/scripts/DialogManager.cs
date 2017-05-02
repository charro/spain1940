using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour {

	public string[] dialogStrings;

	public GameObject franz;
	public GameObject francois;
	public GameObject paquito;
	public GameObject curtain;

	public GameObject dialogsContainer;
	public GameObject leftDialog;
	public Text leftText;
	public GameObject middleDialog;
	public Text middleText;
	public GameObject rightDialog;
	public Text rightText;

	private int currentDialogIndex;

	private bool dialogsEnabled;


	public void SetDialogsEnabled(bool enabled){
		dialogsEnabled = enabled;
	}

	// Use this for initialization
	void Start () {
		currentDialogIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

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

		else {
			if (dialogsEnabled && Input.GetMouseButtonUp (0)) {
				if (StillDialog ()) {
					AdvanceDialog ();
				} else {
					hideDialogs ();
					this.gameObject.SetActive (false);
				}
			}
		}

	}

	private bool StillDialog(){
		return currentDialogIndex < dialogStrings.Length;
	}

	private void AdvanceDialog(){
		string currentDialog = dialogStrings[currentDialogIndex];

		if (currentDialog.StartsWith ("L_")) {
			currentDialog = currentDialog.Remove (0, 2);
			ShowLeftText (currentDialog);
		} else if (currentDialog.StartsWith ("R_")) {
			currentDialog = currentDialog.Remove (0, 2);
			ShowRightText (currentDialog);
		} else {
			currentDialog = currentDialog.Remove (0, 2);
			ShowMiddleText (currentDialog);
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
}
