using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	public delegate void OnDestory ();
	public event OnDestory DestroyCallBack;


	public float offset = 16;
	private bool offScreen;
	private float offScreenX = 0;
	private Rigidbody2D body2d;

	// Use this for initialization
	void Awake () {
		body2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Start () {
		offScreenX = (Screen.width / PerfectCamera.pixelsToUnit) / 2 + offset;

	}

	void Update (){
		var posX = transform.position.x;
		var dirX = body2d.velocity.x;

		if (Mathf.Abs (posX) > offScreenX) {
		
			if(dirX < 0 && posX < -offScreenX){
				offScreen = true;

			}else if(dirX > 0 && posX > offScreenX){

				offScreen = true;
			}
			else{
				offScreen = false;
			}

			if(offScreen){
				OnOutOfBounds();

			}
		}
	}


	public void OnOutOfBounds(){
		offScreen = false;
		GameObjectUtil.Destroy(gameObject);

		if (DestroyCallBack != null) {
			DestroyCallBack();
		}
	}
}
