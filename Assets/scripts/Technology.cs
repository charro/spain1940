using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Technology: MonoBehaviour
{
	public TechnologyType technologyType;
	public  TechnologyType[] parentTechnologies;
	public string name;
	public bool alreadyResearched;
	public Sprite technologySprite;
	public int turnsNeeded;
	[TextArea(3,10)]
	public string description;
	public int actionsNeeded;

	public void OpenTechnologyDetail(){
		FindObjectOfType<TechnologiesPanel> ().ShowTechnologyDetails (this);
	}
}