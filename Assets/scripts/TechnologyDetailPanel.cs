using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TechnologyDetailPanel : MonoBehaviour {

	public Image image;
	public Text name;
	public Text turnsNeeded;
	public Text description;
	public Text actionsNeeded;
	public Button doResearchButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenDetailResearchPanel(Technology technology){
		gameObject.SetActive (true);
		// Set here all info concerning this technology
		image.sprite = technology.technologySprite;
		name.text = technology.name;
		turnsNeeded.text = "It will take " + technology.turnsNeeded + " turns";
		description.text = technology.description;
		actionsNeeded.text = technology.actionsNeeded + "";

		FindObjectOfType<ResearchManager> ().SetSelectedTechnology (technology);

		doResearchButton.interactable = IsResearchEnabled (technology);
	}

	private bool IsResearchEnabled(Technology technology){
		int availableActionPoints = FindObjectOfType<EconomyManager> ().getAvailableActionPoints ();

		return !technology.alreadyResearched && (availableActionPoints >= technology.actionsNeeded);
	}
}
