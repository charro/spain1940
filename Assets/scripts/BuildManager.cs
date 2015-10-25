using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public BuildPanel buildPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseActionLevel(){
		Region selectedRegion = FindObjectOfType<GameManager> ().GetSelectedRegion ();
		selectedRegion.IncreaseActionGenerationLevel ();

		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();
		economyManager.decreaseActionPoints (1);
		economyManager.recalculateMaximumActionsPerTurn ();
		buildPanel.RefreshElements();
	}
}
