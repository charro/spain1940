using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public string[] dialogStrings;

	public GameObject franz;
	public GameObject francois;
	public GameObject paquito;
	public GameObject curtain;

	public GameObject dialogsContainer;
	public GameObject leftDialog;
	public Text leftText;
	public GameObject rightDialog;
	public Text rightText;

	private int currentDialogIndex;

	// Use this for initialization
	void Start () {
		currentDialogIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (FindObjectOfType<LevelManager> ().storyFinished && Input.GetMouseButtonUp (0)) {
			if (StillDialog ()) {
				AdvanceDialog ();
			} 
			else {
				// Show Nazi flag and let game ready to go to gameplay
				hideDialogs ();
				francois.gameObject.SetActive (true);
				franz.gameObject.SetActive (true);
				curtain.gameObject.SetActive (true);
				this.gameObject.SetActive (false);
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
		} else {
			currentDialog = currentDialog.Remove (0, 2);
			ShowRightText (currentDialog);
		}

		currentDialogIndex++;
	}

	private void ShowLeftText(string newtext){
		dialogsContainer.gameObject.SetActive (true);
		leftDialog.gameObject.SetActive (true);
		francois.gameObject.SetActive (true);
		franz.gameObject.SetActive (false);
		rightDialog.gameObject.SetActive (false);
		leftText.text = newtext;
	}

	private void ShowRightText(string newtext){
		dialogsContainer.gameObject.SetActive (true);
		leftDialog.gameObject.SetActive (false);
		francois.gameObject.SetActive (false);
		franz.gameObject.SetActive (true);
		rightDialog.gameObject.SetActive (true);
		rightText.text = newtext;
	}

	private void hideDialogs(){
		dialogsContainer.gameObject.SetActive (false);
	}
}
