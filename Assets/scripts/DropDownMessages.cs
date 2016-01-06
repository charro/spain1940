using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropDownMessages : MonoBehaviour {

	public GameObject messagesPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable(){
		messagesPanel.SetActive (false);
	}

	public void ShowDropDownMessageForSecs(string message, float secs){
		StartCoroutine (ShowPopUpMessageForSecs(message, secs));
	}


	/*********************  PRIVATE METHODS *****************************************/
	private IEnumerator ShowPopUpMessageForSecs(string message, float secs){
		ShowPopUpMessage (message);
		yield return new WaitForSeconds (secs);
		HidePopUpMessage ();
	}
	
	private void ShowPopUpMessage(string message){
		if(messagesPanel.activeSelf){
			messagesPanel.GetComponent<Animator> ().SetBool("show", true);
		}
		else{
			messagesPanel.SetActive (true);
		}
		messagesPanel.GetComponentInChildren<Text> ().text = message;
	}
	
	private void HidePopUpMessage(){
		messagesPanel.GetComponent<Animator> ().SetBool("show", false);
	}
}
