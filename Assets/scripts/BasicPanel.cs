using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasicPanel : MonoBehaviour {

	public Text actionPointsText;
	public Text recruitmentPointsText;

	private EconomyManager economyManager;

	// Use this for initialization
	void Start () {
		if(economyManager == null){
			economyManager = FindObjectOfType<EconomyManager>();
		}

		actionPointsText.text = "AVAILABLE ACTIONS: " + economyManager.getAvailableActions ();
		recruitmentPointsText.text = "RECRUITMENT POINTS: " + economyManager.getRecruitmentPoints ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
