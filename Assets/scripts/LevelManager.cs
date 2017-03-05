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

	// Use this for initialization
	void Start () {
		naziFlagShown = false;
		storyFinished = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp (0)){
			// Hide the cover once is clicked
			if (cover.activeSelf) {
				cover.SetActive (false);
				canvas.SetActive (true);
				storyContainer.SetActive (true);
			} 
			else if(storyFinished && storyContainer.activeSelf){
				storyContainer.SetActive (false);
			}
			else if(naziFlagShown){
				changeScene ("ejpanya");
			}
		}
	}

	public void changeScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void exitGame(){
		Application.Quit ();
	}
}
