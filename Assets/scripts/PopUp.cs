using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUp : MonoBehaviour {

	public Text titleText;
	public Text bodyText;
	private PopUpType type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewPopUp(PopUpType popUpType){
		type = popUpType;

		switch(type){
			case PopUpType.ConfirmPassTurn:
				titleText.text = "Confirm Pass Turn";
				bodyText.text = "You still have some action points. Do you really want to pass turn ?";	
				break;
		}
	}

	public void YesClicked(){
		GameManager gameManager = FindObjectOfType<GameManager> ();
		FindObjectOfType<UIManager> ().HidePopUp ();

		switch(type){
			case PopUpType.ConfirmPassTurn:
				gameManager.EndCurrentTurn();
				break;
		}
	}

	public void NoClicked(){
		FindObjectOfType<UIManager>().HidePopUp();;

		switch(type){
			case PopUpType.ConfirmPassTurn:
				break;
		}
	}
}
