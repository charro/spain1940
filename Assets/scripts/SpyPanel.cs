using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpyPanel : MonoBehaviour {

	public GameObject spyText;
	public GameObject regionImage;
	public GameObject newSpyButton;

	// Use this for initialization
	void Start () {
		// Suscribe to different events
		EventManager.OnNoMoreActions += DisableNewSpyButton;
		EventManager.OnPassTurn += PassedTurn;
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

	// Enable/Disable the new Spy button (usually because there are no actions available)
	public void DisableNewSpyButton(){
		newSpyButton.GetComponent<Button> ().interactable = false;
	}

	// Called automatically by Event Manager
	public void PassedTurn(){
		newSpyButton.GetComponent<Button> ().interactable = true;
	}

}
