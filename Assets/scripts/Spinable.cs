using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinable : MonoBehaviour {

	public Sprite currentSprite;
	public Sprite newSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSprites(Sprite currentSp, Sprite newSp){
		currentSprite = currentSp;
		GetComponent<Image>().sprite = currentSprite;
		newSprite = newSp;
	}

	public void changeSprite(){
		GetComponent<Image>().sprite = newSprite;
	}
}
