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
			case PopUpType.ConfirmRecruitment:
				titleText.text = "Confirm Recruitment";
				bodyText.text = "This will consum 1 Action Point. Do you confirm the recruitment ?";	
				break;
		case PopUpType.ConfirmBuild:
			titleText.text = "Confirm Build";
			bodyText.text = "This will consum 1 Action Point. Do you confirm the build ?";	
			break;
		case PopUpType.ConfirmNewSpy:
			titleText.text = "Confirm Spy Region";
			bodyText.text = "This will consum 1 Action Point. Do you confirm to send the spy ?";	
			break;
		}
	}

	public void YesClicked(){
		FindObjectOfType<UIManager> ().HidePopUp ();

		switch(type){
			case PopUpType.ConfirmPassTurn:
				FindObjectOfType<GameManager> ().EndCurrentTurn();
				break;
			case PopUpType.ConfirmRecruitment:
				FindObjectOfType<RecruitmentManager>().PerformRecruitment();
				break;
			case PopUpType.ConfirmBuild:
				FindObjectOfType<BuildManager>().IncreaseActionLevel();
				break;
		case PopUpType.ConfirmNewSpy:
				FindObjectOfType<SpyManager>().AddNewSpy();
				break;
		}
	}

	public void NoClicked(){
		FindObjectOfType<UIManager>().HidePopUp();;

		switch(type){
			case PopUpType.ConfirmPassTurn:
			case PopUpType.ConfirmRecruitment:
			case PopUpType.ConfirmBuild:
				break;
			case PopUpType.ConfirmNewSpy:
				FindObjectOfType<SpyManager>().EndNewSpyCreation();
				break;
		}
	}
}
