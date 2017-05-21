using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// When clicked, it calls the OpenTechnologyDetail() method of the corresponding technology
public class TechnologyButton : Button {

	public Technology associatedTechnology;
	public Image technologyImage;

	public void Awake(){
		technologyImage.sprite = associatedTechnology.technologySprite;
		base.Awake ();
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
		if(interactable){
			associatedTechnology.OpenTechnologyDetail ();
			FindObjectOfType<SoundManager> ().PlayClick ();
		}
	}
		
	void OnEnable()
	{
		technologyImage.sprite = associatedTechnology.technologySprite;
		base.OnEnable ();
	}
}
