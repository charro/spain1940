using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnMapMessages : MonoBehaviour {

	public GameObject bottomRightText;

	// Use this for initialization (only first time object is enabled)
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HideAll(){
		bottomRightText.SetActive(false);
	}

	public void showWhereToMoveText(){
		showBottomRightText ("Choose where to move troops");
	}

	public void showWhereToSpyText(){
		showBottomRightText ("Choose where to send spy");
	}

	public void showWhereToAttackText(){
		showBottomRightText ("Choose FROM where to send attack");
	}

	public void showBottomRightText(string textToShow){
		UIManager.ShowOnMapMessagesPanel();
		bottomRightText.GetComponent<Text> ().text = textToShow;
		bottomRightText.SetActive(true);
	}

}
