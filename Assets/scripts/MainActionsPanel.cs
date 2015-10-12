using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainActionsPanel : MonoBehaviour {

	public Button infoButton;
	public Button buildButton;
	public Button moveTroopsButton;
	public Button recruitButton;

	void OnEnable(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		EconomyManager economyManager = FindObjectOfType<EconomyManager> ();

		Region selectedRegion = gameManager.GetSelectedRegion ();
		bool hasAnyTroops = (selectedRegion != null && selectedRegion.HasAnyTroops());
		bool hasAnyActionPoints = (economyManager.getAvailableActionPoints() > 0);

		buildButton.interactable = hasAnyActionPoints;
		moveTroopsButton.interactable = hasAnyActionPoints && hasAnyTroops;
		recruitButton.interactable = hasAnyActionPoints;
	}
}
