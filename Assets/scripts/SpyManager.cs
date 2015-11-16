using UnityEngine;
using System.Collections;

public class SpyManager : MonoBehaviour {

	public int MAX_ACTIVE_SPIES;
	private Spy[] activeSpies;

	// Level selected from UI to create a new Spy
	private int lastSelectedSpyLevel;
	private Region lastSelectedSpiedRegion;

	// Use this for initialization
	void Start () {
		activeSpies = new Spy[MAX_ACTIVE_SPIES];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SelectNewSpyLevel(int newLevel){
		lastSelectedSpyLevel = newLevel;
		FindObjectOfType<GameStateMachine> ().ChangeToState (GameState.NewSpyState);
	}

	public void ConfirmAddNewSpyToRegion(Region region){
		lastSelectedSpiedRegion = region;
		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmNewSpy);
	}

	//public void AddNewSpy(RegionType spiedRegion, int spyLevel){
	public void AddNewSpy(){

		for(int i=0; i<activeSpies.Length; i++){
			if(activeSpies[i] == null){
				Spy newSpy = ScriptableObject.CreateInstance<Spy>();
				newSpy.InitializeSpy(lastSelectedSpiedRegion, lastSelectedSpyLevel);
				activeSpies[i] = newSpy;
				FindObjectOfType<EconomyManager>().decreaseActionPoints(1);
				FindObjectOfType<PopUpMessages>().
						ShowDropDownMessageForSecs("Spy of level " + lastSelectedSpyLevel + 
					                           " on his way to region " + lastSelectedSpiedRegion.name, 5);
				EndNewSpyCreation ();
				return;
			}
		}

		EndNewSpyCreation ();
	}

	public void EndNewSpyCreation(){
		FindObjectOfType<GameStateMachine> ().ChangeToState (GameState.IdleMapState);
	}

	public void RemoveSpy(Spy spyToRemove){
		for(int i=0; i<activeSpies.Length; i++){
			if(activeSpies[i] == spyToRemove){
				activeSpies[i] = null;
			}
		}
	}

	public Spy[] GetActiveSpies(){
		return activeSpies;
	}
	
}