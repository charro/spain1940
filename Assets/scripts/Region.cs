﻿using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {

	public bool selected;
	public Sprite spanishRegionSprite;
	public Sprite naziRegionSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleSelected(){
		selected = !selected;
		Animator animator = GetComponent<Animator>();
		animator.SetBool ("selected", selected);

		if(selected){
			UIManager.regionSelected();
		}
		else{
			UIManager.regionUnselected();
		}
	}

	public bool isSelected(){
		return selected;
	}

	public void SetRegionSprite(bool naziConquered){

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		
		if(naziConquered){
			spriteRenderer.sprite = naziRegionSprite;
		}
		else{
			spriteRenderer.sprite = spanishRegionSprite;
		}
	}
}
