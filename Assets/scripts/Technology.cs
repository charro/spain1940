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

	public bool AreParentsResearched(){
		if(parentTechnologies.Length == 0){
			return true;
		}
		else{
			bool allParentsResearched = true;
			ResearchManager researchManager = FindObjectOfType<ResearchManager>();

			foreach(TechnologyType parent in parentTechnologies){
				if(!researchManager.IsAlreadyResearched(parent)){
					allParentsResearched = false;
				}
			}

			return allParentsResearched;
		}
	}

	public string ToString(){
		return name;
	}
}