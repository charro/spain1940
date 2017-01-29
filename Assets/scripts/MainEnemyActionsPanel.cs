using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainEnemyActionsPanel : MonoBehaviour {

	public Button infoButton;
	public Button attackButton;


	void Start(){
		Debug.Log ("Here we are");
	}

	void OnEnable(){
		GameManager gameManager = FindObjectOfType<GameManager> ();

		Region selectedRegion = gameManager.GetSelectedRegion ();

		attackButton.interactable = selectedRegion.HasEnemyTroopsInBorderRegions () && FindObjectOfType<EconomyManager>().getAvailableActionPoints() > 0;
	}
}
