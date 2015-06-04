using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {

	public bool selected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleSelected(){
		selected = !selected;
		Animator animator = GetComponent<Animator>();
		animator.SetBool ("selected", selected);
	}

	public bool isSelected(){
		return selected;
	}
}
