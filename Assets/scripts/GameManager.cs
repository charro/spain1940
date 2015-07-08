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
		UIManager.ShowUIPanelsOnRegionSelected(newRegionSelected.isNazi);
	}

	public void RegionUnselected(){
		selectedRegion = null;
		UIManager.HidePanelsOnRegionUnselected();
	}

	public Region GetSelectedRegion(){
		return selectedRegion;
	}

	public bool CanRegionBeTouched(Region region){

		// Do we have some region selected and we're in Main Menu?
		if(selectedRegion != null && UIManager.IsMainActionsShown()){
			return region == selectedRegion;
		}
		// Not in main menu, but, do we have any UI Panel over the map ?
		else if(UIManager.IsAnyGUIPanelShown()){
			return false;
		}
		// No GUI shown over the map. We can touch the region
		else{
			return true;
		}
	}
}