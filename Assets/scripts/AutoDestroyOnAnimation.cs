using UnityEngine;
using System.Collections;

public class AutoDestroyOnAnimation : StateMachineBehaviour {

	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Transform transform = animator.transform;

		// Kill parent Object
		Destroy(transform.parent.gameObject);
	}
}
