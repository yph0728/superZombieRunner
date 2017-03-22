using UnityEngine;
using System.Collections;

public class PerfectCamera : MonoBehaviour {

	public static float pixelsToUnit = 1f;
	public static float scale = 1f;


	//what is vector 2 
	public Vector2 nativeResolution = new Vector2(240, 160);

	// gets called before starts
	void Awake () {
		//reference of the camera
		var camera = GetComponent<Camera> ();

		if (camera.orthographic) {
			// scale =  current height of the screen and divied by native resolution
			scale = Screen.height / nativeResolution.y; 
			//modify the pixel per unit to relate 
			pixelsToUnit *= scale;
			//half?
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnit;
		}
	}
}
