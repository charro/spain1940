using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchSpyPanel : MonoBehaviour {

	public GameObject spyPanel;
	public GameObject researchPanel;
	public GameObject newSpyPanel;

	// Use this for initialization
	void Start () {
		// Hide Spy Panel in case is visible
		newSpyPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
