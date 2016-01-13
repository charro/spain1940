using UnityEngine;
using System.Collections;

public class RecruitPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		FindObjectOfType<RecruitmentManager> ().RefreshUI ();
	}
}
