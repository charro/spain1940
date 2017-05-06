using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAnimationOnEnable : MonoBehaviour {

	public void OnEnable(){
		GetComponent<Animator>().SetTrigger ("TriggerAnim");
	}
}
