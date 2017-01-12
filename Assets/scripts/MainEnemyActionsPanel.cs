using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainEnemyActionsPanel : MonoBehaviour {

	public Button infoButton;
	public Button attackButton;

	void OnEnable(){
		GameManager gameManager = FindObjectOfType<GameManager> ();

		Region selectedRegion = gameManager.GetSelectedRegion ();

		attackButton.interactable = selectedRegion.HasEnemyTroopsInBorderRegions ();
	}
}
