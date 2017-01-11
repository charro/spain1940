using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject cover;
	public GameObject canvas;
	public bool coverClicked;

	// Use this for initialization
	void Start () {
		coverClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!coverClicked && 
			(Input.touchCount > 0 || Input.GetMouseButtonUp(0))
		   ) {
			coverClicked = true;
			cover.gameObject.SetActive (false);
			canvas.gameObject.SetActive (true);
		}
	}

	public void changeScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void exitGame(){
		Application.Quit ();
	}
}
