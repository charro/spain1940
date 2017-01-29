using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject cover;
	public GameObject canvas;
	public bool coverClicked;
	public bool naziFlagShown;

	// Use this for initialization
	void Start () {
		coverClicked = false;
		naziFlagShown = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(cover){
			// Hide the cover once is clicked
			if (!coverClicked){
				if (Input.GetMouseButtonUp (0)) {
					coverClicked = true;
					cover.gameObject.SetActive (false);
					canvas.gameObject.SetActive (true);
				}
			}
			else{
				if (naziFlagShown &&
					Input.GetMouseButtonUp (0)) {
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
}
