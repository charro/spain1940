using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject midBackground;

	private static UIManager singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void showMidBackground(bool show){
		singleton.midBackground.SetActive (show);
	}

	public static void regionSelected(){
		showMidBackground(true);

		// Disable here input for all regions
	}

	public static void regionUnselected(){
		showMidBackground(false);

		// Enable here input again for all regions
	}
}