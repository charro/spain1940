using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// The region that we are currently working in
	private Region selectedRegion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RegionSelected(Region newRegionSelected){
		selectedRegion = newRegionSelected;
		UIManager.regionSelected(newRegionSelected.isNazi);
	}

	public void RegionUnselected(){
		selectedRegion = null;
		UIManager.regionUnselected();
	}

	public Region GetSelectedRegion(){
		return selectedRegion;
	}
}