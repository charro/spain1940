using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void SpiedFinishedAction();
	public static event SpiedFinishedAction OnSpyingFinished;

	public delegate void ResearchFinishedAction();
	public static event ResearchFinishedAction OnResearchFinished;

	public delegate void PassTurnAction();
	public static event PassTurnAction OnPassTurn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void TriggerSpyingFinishedEvent(){
		OnSpyingFinished ();
	}

	public static void TriggerResearchFinishedEvent(){
		OnResearchFinished ();
	}

	public static void TriggerPassTurnEvent(){
		if(OnPassTurn != null){
			OnPassTurn ();
		}
	}

}
