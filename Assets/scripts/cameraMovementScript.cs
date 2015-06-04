using UnityEngine;
using System.Collections;

public class cameraMovementScript : MonoBehaviour {

	public bool isZooming = false;
	public float normal;
	public float zoom;
	public float smooth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera zoomCamera = GetZoomingCamera ();

		if(zoomCamera != null){
			if(isZooming == true){
				if(!zoomCamera.enabled){
					zoomCamera.enabled = true;
					Camera.main.enabled = false;
				}
				zoomCamera.fieldOfView = Mathf.Lerp(zoomCamera.fieldOfView,zoom,Time.deltaTime*smooth);
			}
			else{
				if(zoomCamera.enabled){
					zoomCamera.enabled = false;
					Camera.main.enabled = true;
				}
				zoomCamera.fieldOfView = Mathf.Lerp(zoomCamera.fieldOfView,normal,Time.deltaTime*smooth);
			}
		}

	}

	Camera GetZoomingCamera() {

		foreach(Camera camera in Camera.allCameras)
		{
			if(camera.name.Equals("ZoomCamera")){
				return camera;
			}
		}

		return null;
	}
}
