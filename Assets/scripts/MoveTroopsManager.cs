using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveTroopsManager : MonoBehaviour {

	public Text toRegionText;
	public Text fromRegionText;

	private Region fromRegion;
	private Region toRegion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetFromRegion(Region region){
		fromRegion = region;
		fromRegionText.text = fromRegion.name;
	}

	public void SetToRegion(Region region){
		toRegion = region;
		toRegionText.text = toRegion.name;
	}
}
