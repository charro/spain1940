using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject cover;
	public GameObject canvas;
	public GameObject storyContainer;

	public bool naziFlagShown;
	public bool storyFinished;

	void Awake () {
		naziFlagShown = false;
		storyFinished = false;

		if(SceneManager.GetActiveScene ().name == "intro") {
			// Load here the savegame data and decide if showing intro or not
			SaveGameManager.Load();

			if (SaveGameManager.GetGameData ().tutoDone) {
				changeScene ("ejpanya");
			}
		}
	}

	void Update () {

		if (SceneManager.GetActiveScene ().name == "intro") {
			
			if (Input.GetMouseButtonUp (0)) {
				// Hide the cover once is clicked
				if (cover.activeSelf) {
					cover.SetActive (false);
					canvas.SetActive (true);
					storyContainer.SetActive (true);
				} else if (storyFinished && storyContainer.activeSelf) {
					storyContainer.SetActive (false);
					FindObjectOfType<SoundManager> ().PlayFrenchySong ();
				} else if (naziFlagShown) {
					changeScene ("ejpanya");
				}
			}

		}
	}

	public void changeScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void exitGame(){
		Application.Quit ();
	}

	public void SurrenderButtonClicked(){
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.RestartGame);
	}

	public void RestartGame(){
		SaveGameManager.Delete ();
		changeScene ("intro");
	}
}
