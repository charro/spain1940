using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text dateText;
	public Text actionPointsText;
	public Text recruitmentPointsText;
	public GameObject messagesPanel;

	// Use this for initialization
	void Start () {
		Refresh ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		Refresh ();
	}

	void OnDisable(){
		messagesPanel.SetActive (false);
	}

	public void Refresh(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();

		dateText.text = "TURN " + gameManager.GetCurrentTurnNumber ();
		actionPointsText.text = "AVAILABLE ACTIONS KK: " + economyManager.getAvailableActionPoints ();
		recruitmentPointsText.text = "RECRUITMENT POINTS: " + economyManager.getRecruitmentPoints ();
	}

	public void ShowDropDownMessageForSecs(string message, float secs){
		StartCoroutine (ShowPopUpMessageForSecs(message, secs));
	}

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