using UnityEngine;
using System.Collections;

public class TechnologiesPanel : MonoBehaviour {

	public GameObject technologyDetailPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		technologyDetailPanel.SetActive (false);
		EnableAvailableTechnologies ();
	}

	public void Hide(){
		gameObject.SetActive (false);
	}

	public void ShowTechnologyDetails(Technology technology){
		technologyDetailPanel.GetComponent<TechnologyDetailPanel> ().OpenDetailResearchPanel (technology);
	}

	public void EnableAvailableTechnologies(){
		foreach(TechnologyButton button in GetComponentsInChildren<TechnologyButton>()){
			Technology technology = button.associatedTechnology;
			bool enableButton = technology.alreadyResearched || technology.AreParentsResearched();

			button.interactable = enableButton;
		}
	}
}
