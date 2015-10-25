using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text dateText;
	public Text actionPointsText;
	public Text recruitmentPointsText;

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

	public void Refresh(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();

		dateText.text = "TURN " + gameManager.GetCurrentTurnNumber ();
		actionPointsText.text = "AVAILABLE ACTIONS KK: " + economyManager.getAvailableActionPoints ();
		recruitmentPointsText.text = "RECRUITMENT POINTS: " + economyManager.getRecruitmentPoints ();
	}
	
}