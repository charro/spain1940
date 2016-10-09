﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void exitGame(){
		Application.Quit ();
	}
}
