using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CurtainAnimationFinished(){
		// Inform the LevelManager that all is ready to go to Gameplay
		FindObjectOfType<LevelManager> ().naziFlagShown = true;
	}
}
