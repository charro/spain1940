using UnityEngine;
using System.Collections;

public class AutoDestroyOnAnimation : StateMachineBehaviour {

	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Transform transform = animator.transform;

		while(transform != null && transform.parent != null){
			transform = transform.parent;
		}

		// Kill parent Object
		Destroy(transform.gameObject);
	}
}
