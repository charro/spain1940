using UnityEngine;
using System.Collections;

public class ObjectClickChecker : MonoBehaviour {

	private RuntimePlatform platform;

	// Use this for initialization
	void Start () {
		platform = Application.platform;
	}
	
	// Update is called once per frame
	void Update () {
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					checkTouch(Input.GetTouch(0).position);
				}
			}
		}else if(platform == RuntimePlatform.WindowsEditor){
			if(Input.GetMouseButtonDown(0)) {
				checkTouch(Input.mousePosition);
			}
		}
	}

	void checkTouch(Vector2 pos){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
		//RaycastHit hit;

		// if (Physics.Raycast (ray, out hit, layMask)) {
		//	RaycastHit2D ahit = Physics2D.GetRayIntersection (ray, 15f, tileMask);
		RaycastHit2D ahit = Physics2D.GetRayIntersection (ray);
		if (ahit) {
			GameObject touched = ahit.transform.gameObject;
			//ahit.transform.gameObject.SendMessage ("Suicide");
			Debug.Log("Touched object by Raycast: " + touched.name);

			// Check the kind of component touched and act in consequence
			Region region = touched.GetComponent<Region>();
			if ( region ) {
				regionTouched(region);
			}

			/* RecruitedUnitGroup recruitedUnitGroup = touched.GetComponent<RecruitedUnitGroup>();
			if( recruitedUnitGroup ){
				recruitedUnitGroup.Touched();
			}*/
		}
		//}

		/*
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if(hit){
			Debug.Log("Touched object by Overlap: " + hit.transform.gameObject.name);
			hit.transform.gameObject.SendMessage("Clicked",0,SendMessageOptions.DontRequireReceiver);
		}
		*/
	}

	// Actions to do if we touched a Region
	void regionTouched(Region region){
		region.toggleSelected();
		GUIManager.showMainActions (region.isSelected());
	}
}



