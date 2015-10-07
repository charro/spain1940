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
		Region selectedRegion = gameManager.GetSelectedRegion ();
		if(selectedRegion != null){
			moveTroopsButton.interactable = selectedRegion.HasAnyTroops();
		}
	}
}
