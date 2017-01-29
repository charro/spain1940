using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text dateText;
	public Text actionPointsText;
	public Text actionTurnPointsText;
	public Text recruitmentPointsText;
	public Text recruitmentTurnPointsText;
	public Text spyButtonText;
	public Text researchText;
	public Image researchIcon;

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
		actionPointsText.text = "ACTIONS: " + economyManager.getAvailableActionPoints ();
		actionTurnPointsText.text = "+ " + economyManager.GetNewActionsPerTurn () + " / TURN";
		recruitmentPointsText.text = "RECR. POINTS: " + economyManager.getMilitaryPoints ();
		recruitmentTurnPointsText.text = "+ " + economyManager.GetNewRecPointsPerTurn() + " / TURN";
		spyButtonText.text = "SPIES SENT: " + spyManager.GetNumberOfSpiesSent ();

		ResearchManager researchManager = FindObjectOfType<ResearchManager> ();
		if(researchManager.IsAnyTechnologyBeingResearched()){
			researchText.text = "RESEARCHING:";
			researchIcon.gameObject.SetActive(true);
			researchIcon.sprite = researchManager.GetCurrentResearchedTechnology ().technologySprite;
		}
		else{
			researchText.text = "NO RESEARCH ONGOING";
			researchIcon.gameObject.SetActive(false);
		}
	}
	
}