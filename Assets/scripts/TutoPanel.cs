using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoPanel : MonoBehaviour {

	public RectTransform arrow;
	public RectTransform actionsPanel;
	public RectTransform militaryPanel;
	public RectTransform researchPanel;
	public Text tutoText;

	// Explanation Panel
	public GameObject explanationPanel;
	public GameObject explanationPart1;
	public GameObject explanationPart2;
	public GameObject explanationPart3;

	private int currentTutoStep = 0;

	// Use this for initialization
	void Awake () {
		if (SaveGameManager.GetGameData ().tutoDone) {
			this.gameObject.SetActive(false);
		} else {
			StartTutorial ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene ().name == "ejpanya" && 
			Input.GetMouseButtonUp (0)){
			// Advance with tutorial
			NextTutoStep();
		}
	}

	void NextTutoStep(){

		switch(currentTutoStep){
		case 0:
			tutoText.text = "Hola Chief ! I'm Paquito. Welcome to the war zone ! It looks we're having some problems with those Nazi dickheads. Let me explain how we can face them";
			break;
		case 1:
			arrow.gameObject.SetActive (true);
			tutoText.text = "Each thing you do in your turn will cost actions. Here we have the remaining actions for this turn and the available actions you'll have each turn. To have more Actions points we need to build action buildings in our regions";
			PointArrowTo (actionsPanel);
			break;
		case 2:
			tutoText.text = "To make our army we need to generate Military Points. Each new unit you recruit cost Military points. To have more Military points we need to build military buildings in our regions";
			PointArrowTo (militaryPanel);
			break;
		case 3:
			tutoText.text = "To have new buildings, army units or other technologies, you have to research them first. Clicking here you will be able to start new researchs and also to send spies";
			PointArrowTo (researchPanel);
			break;
		case 4:
			tutoText.text = "So, remember Chief, we need to increase our forces to defend our regions from the Nazi attacks, and also to recover the regions that have been controlled by Nazi troops already";
			arrow.gameObject.SetActive (false);
			break;
		case 5:
			explanationPanel.gameObject.SetActive (true);
			tutoText.text = "The steps are clear, Chief: First, research new technologies as buildings or army units. You can also research spying or other technologies that improve our troops";
			break;
		case 6:
			explanationPart2.gameObject.SetActive (true);
			tutoText.text = "In second place, build the researched buildings in our regions so we get more actions and more military points per turn";
			break;
		case 7:
			explanationPart3.gameObject.SetActive (true);
			tutoText.text = "Last, but not least, accumulate as much military points as possible and get a lot of army units to reinforce our army";
			break;
		case 8:
			explanationPanel.gameObject.SetActive (false);
			tutoText.text = "And remember Chief, everything that you do will cost action points, so think well about what to do before doing it !!";
			break;
		case 9:
			tutoText.text = "Good luck mate!! We have to save our country from those fucking square-heads !!! Glory to Spain !!!";
			break;
		case 10:
			EndTutorial ();
			break;
		}

		currentTutoStep++;
	}

	void StartTutorial(){
		NextTutoStep();
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.TutorialState);
	}

	void EndTutorial(){
		SaveGameManager.GetGameData ().tutoDone = true;
		SaveGameManager.Save ();

		this.gameObject.SetActive(false);
		FindObjectOfType<GameStateMachine> ().SwitchBackToPreviousState ();
	}

	void PointArrowTo(RectTransform uiElement){
		Vector2 newPosition = new Vector2 (arrow.anchorMax.x, uiElement.anchorMax.y);
		arrow.anchorMax = newPosition;
		Vector2 newPosition2 = new Vector2 (arrow.anchorMin.x, uiElement.anchorMin.y);
		arrow.anchorMin = newPosition2;
	}
}