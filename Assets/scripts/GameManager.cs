using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// The region that we are currently working in
	private Region selectedRegion;

	// All the regions in the game
	private Dictionary<RegionType, Region> allRegions = new Dictionary<RegionType, Region>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddRegionToList(Region newRegion){
		allRegions.Add (newRegion.regionType, newRegion);
		Debug.Log ("GameManager: Added new Region to RegionList: " + newRegion);
	}

	public void ShowMapAllRegionsDisabled(){
		// Hide all UI panels, showing the map that's behind
		UIManager.hideAllPanels ();
		// Disable all regions
		DisableAllRegions ();
		// If any selected region, send it back to its position
		if(selectedRegion){
			selectedRegion.toggleSelected();
		}
	}

	public void ShowMapAndBasicUI(){
		// Hide all UI panels, showing the map that's behind
		UIManager.hideAllPanels ();
		// Enable all regions
		EnableAllRegions ();
		// If any selected region, send it back to its position
		if(selectedRegion){
			selectedRegion.toggleSelected();
		}
		// Show basic GUI
		UIManager.ShowBasicPanel ();
	}

	public void DisableAllRegions(){
		foreach(Region region in allRegions.Values){
			region.Disable();
		}
	}

	public void EnableAllRegions(){
		foreach(Region region in allRegions.Values){
			region.Enable();
		}
	}

	public void RegionSelected(Region newRegionSelected){
		selectedRegion = newRegionSelected;
		UIManager.onRegionSelected(newRegionSelected);
	}

	public void RegionUnselected(){
		UIManager.onRegionUnselected(selectedRegion);
		selectedRegion = null;
	}

	public Region GetSelectedRegion(){
		return selectedRegion;
	}

	public bool CanRegionBeTouched(Region region){

		// Only enabled regions can be touched
		if(!region.IsEnabled()){
			return false;
		}

		// Do we have some region selected and we're in Main Menu?
		if(selectedRegion != null && UIManager.IsMainActionsShown()){
			// We can touch it only if this is the selected one
			return region == selectedRegion;
		}
		// Not in main menu, but, do we have any UI Panel shown over the map (except the basic Panel)?
		else if(UIManager.IsAnyGUIPanelShownButBasicPanel()){
			return false;
		}
		// No GUI shown over the map. We can touch the region
		else{
			return true;
		}
	}

	public Region[] GetRegionsBorderingRegion(Region region){
		ArrayList regionList = new ArrayList();

		foreach(RegionType regionType in GameManager.GetRegionTypesBordering(region.regionType)){
			if(allRegions.ContainsKey(regionType)){
				regionList.Add(allRegions[regionType]);
			}
		}

		return (Region[]) regionList.ToArray(typeof(Region));
	}

	public static RegionType[] GetRegionTypesBordering(RegionType regionType){
		RegionType[] response = new RegionType[] {};

		switch(regionType){
		 case RegionType.Galicia:
			response = new [] {RegionType.Asturias, RegionType.Leon};
			break;
		case RegionType.Asturias:
			response = new [] {RegionType.Galicia, RegionType.Leon, RegionType.CastillaVieja};
			break;
		case RegionType.CastillaVieja:
			response = new [] {RegionType.Asturias, RegionType.Leon, RegionType.Extremadura, 
				RegionType.CastillaNueva, RegionType.Madrid, RegionType.Vascongadas, 
				RegionType.Aragon};
			break;
		case RegionType.Vascongadas:
			response = new [] {RegionType.CastillaVieja, RegionType.Aragon};
			break;
		case RegionType.Aragon:
			response = new [] {RegionType.Vascongadas, RegionType.CastillaVieja,
				RegionType.CastillaNueva, RegionType.Valencia, RegionType.Catalunya};
			break;
		case RegionType.Catalunya:
			response = new [] {RegionType.Aragon, RegionType.Valencia, RegionType.Baleares};
			break;
		case RegionType.Leon:
			response = new [] {RegionType.Galicia, RegionType.Asturias, RegionType.CastillaVieja, 
				RegionType.Extremadura };
			break;
		case RegionType.Extremadura:
			response = new [] {RegionType.Leon, RegionType.CastillaVieja, 
				RegionType.CastillaNueva, RegionType.Andalucia};
			break;
		case RegionType.CastillaNueva:
			response = new [] {RegionType.CastillaVieja, RegionType.Extremadura,
				RegionType.Andalucia, RegionType.Murcia, RegionType.Valencia,
				RegionType.Aragon, RegionType.Madrid};
			break;
		case RegionType.Madrid:
			response = new [] {RegionType.CastillaVieja, RegionType.CastillaNueva};
			break;
		case RegionType.Valencia:
			response = new [] {RegionType.Asturias, RegionType.Leon, RegionType.Baleares};
			break;
		case RegionType.Murcia:
			response = new [] {RegionType.CastillaNueva, RegionType.Valencia,
				RegionType.Andalucia};
			break;
		case RegionType.Andalucia:
			response = new [] {RegionType.Extremadura, RegionType.CastillaNueva,
				RegionType.Murcia, RegionType.Canarias, RegionType.Marruecos};
			break;
		case RegionType.Marruecos:
			response = new [] {RegionType.Andalucia, RegionType.Canarias};
			break;
		case RegionType.Canarias:
			response = new [] {RegionType.Andalucia, RegionType.Marruecos};
			break;
		}

		return response;
	}
}