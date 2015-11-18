using UnityEngine;
using System.Collections;

public class TechnologyDetailPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenConfirmResearchPanel(int technologyId){
		GetComponentInParent <GameObject> ().SetActive (true);
	}
}
