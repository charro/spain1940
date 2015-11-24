using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// When clicked, it calls the OpenTechnologyDetail() method of the corresponding technology
public class TechnologyButton : Button {
	
	public Technology associatedTechnology;

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
		if(interactable){
			associatedTechnology.OpenTechnologyDetail ();
		}
	}

}
