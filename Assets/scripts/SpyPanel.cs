using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpyPanel : MonoBehaviour {

	public GameObject spyText;
	public GameObject regionImage;
	public GameObject newSpyButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSpy(Spy spy){
		spyText.SetActive(spy != null);
		regionImage.SetActive(spy != null);
		newSpyButton.SetActive(spy == null);

		if(spy != null){
			spyText.GetComponent<Text>().text = "Espia Marcelino\n Still " + spy.turnsToEndSpying + " turns to end";
			regionImage.GetComponent<Image>().sprite = spy.spiedRegion.naziRegionSprite;
		}
	}
}
