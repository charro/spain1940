using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaziAttackScreen : MonoBehaviour {

	public Image attackedRegionImage;

	public void ShowAttackToRegion(Region attackedR1egion){
		this.gameObject.SetActive (true);
		attackedRegionImage.sprite = attackedR1egion.GetCurrentSprite();
	}

	public void GoToCombatScreen(){
		this.gameObject.SetActive (false);
		// Start combat as everything is ready
		FindObjectOfType<CombatManager> ().StartCombat (true);
	}
}
