using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text dateText;
	public Text actionPointsText;
	public Text recruitmentPointsText;
	public Text spyButtonText;
	public Text researchText;

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
		SpyManager spyManager = FindObjectOfType<SpyManager> ();

		dateText.text = "TURN " + gameManager.GetCurrentTurnNumber ();
		actionPointsText.text = "AVAILABLE ACTIONS: " + economyManager.getAvailableActionPoints ();
		recruitmentPointsText.text = "RECRUITMENT POINTS: " + economyManager.getMilitaryPoints ();
		spyButtonText.text = "SPIES SENT: " + spyManager.GetNumberOfSpiesSent ();

		ResearchManager researchManager = FindObjectOfType<ResearchManager> ();
		if(researchManager.IsAnyTechnologyBeingResearched()){
			researchText.text = "CURRENT RESEARCH: \n" + researchManager.GetCurrentResearchedTechnology();
		}
		else{
			researchText.text = "NO RESEARCH ONGOING";
		}
	}
	
}