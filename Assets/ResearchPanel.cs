using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchPanel : MonoBehaviour {

	public Text ResearchInProgressText;
	public Image ResearchInProgressImage;
	public Button newResearchButton;

	// Use this for initialization
	void Start () {
		// Suscribe to different events
		EventManager.OnNoMoreActions += DisableNewResearchButton;
		EventManager.OnPassTurn += PassedTurn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called automatically by Event Manager
	void DisableNewResearchButton(){
		newResearchButton.interactable = false;
	}

	// Called automatically by Event Manager
	public void PassedTurn(){
		newResearchButton.interactable = true;
	}
}
