using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionChangedPanel : MonoBehaviour {

	public Spinable regionImage;
	public Text	regionText;
	public Sprite naziBackground;
	public Sprite repubBackground;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(Region region){
		if (region.isNazi) {
			regionImage.setSprites (region.naziRegionSprite, region.republicanRegionSprite);
			regionText.text = region.name + " RECOVERED";
			GetComponent<Image> ().sprite = repubBackground;
		} 
		else {
			regionImage.setSprites (region.republicanRegionSprite, region.naziRegionSprite);
			regionText.text = region.name + " LOST";
			GetComponent<Image> ().sprite = naziBackground;
		}

		gameObject.SetActive (true);
	}
		
}
