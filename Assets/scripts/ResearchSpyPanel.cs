using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchSpyPanel : MonoBehaviour {

	public GameObject spiesPanel;
	public GameObject researchPanel;
	public GameObject newSpyPanel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		// Ensure to Hide Spy Panel in case is visible
		newSpyPanel.SetActive (false);
		// Refresh All Spying Info
		spiesPanel.GetComponent<SpiesPanel> ().RefreshSpiesInfo ();
	}
}
