using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour {

	private Region attackingRegion;
	private Region attackedRegion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetAttackedRegion(Region region){
		attackedRegion = region;
	}

	public Region GetAttackedRegion(){
		return attackedRegion;
	}

	public Region GetAttackingRegion(){
		return attackingRegion;
	}

	public void StartCombat(){
		// IN DEVELOPMENT. BY THE MOMENT, AUTOMATIC WIN
		attackedRegion.SetNaziConquered (false);

		FindObjectOfType<EconomyManager>().decreaseActionPoints(1);
		// End combat and back to Idle map
		FindObjectOfType<GameStateMachine> ().SwitchToState (GameState.IdleMapState);
	}

	public void ConfirmStartAttackFrom(Region region){
		// The attacking region is the just clicked region
		attackingRegion = region;

		FindObjectOfType<UIManager>().ShowPopUp(PopUpType.ConfirmAttack);
	}
}
