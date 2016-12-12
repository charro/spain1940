using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text dateText;
	public Text actionPointsText;
	public Text recruitmentPointsText;
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
		actionPointsText.text = "AVAILABLE ACTIONS: \n" + economyManager.getAvailableActionPoints ();
		recruitmentPointsText.text = "RECRUITMENT POINTS: \n" + economyManager.getMilitaryPoints ();
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