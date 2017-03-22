using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;

	private bool gameStarted;
	private bool beatBestTime;

	public Text continueText;
	public Text scoreText;

	private float timeElapsed = 0f;
	private float bestTime =0f;


	private float blinkTime = 0f;
	private bool blink;

	// Use this for initialization
	void Awake () {
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager> ();
	}
	
	// Update is called once per frame
	void Start () {
		//re aling the floor

		var floorHeight = floor.transform.localScale.y;


		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height/PerfectCamera.pixelsToUnit) / 2 ) + (floorHeight /2);

		floor.transform.position = pos;
	

		spawner.active = false;

		Time.timeScale = 0;


		continueText.text = "PRESS ANY BUTTON TO START";
		//continueText.text.co

		bestTime = PlayerPrefs.GetFloat("BestTime");
	}

	void Update(){
		if (!gameStarted && Time.timeScale == 0) {
			if(Input.anyKeyDown){
				timeManager.ManipulateTime(1,1f);
				ResetGame ();
			}
		
		}

		if (!gameStarted) {
			blinkTime++;
			if (blinkTime % 40 == 0) {
				blink = !blink;
			}
			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);


			var textColor = beatBestTime ? "#FF0" : "#FFF";
			scoreText.text = "Time: " + FormatTime (timeElapsed) + "\n<color="+textColor+">Best: " + FormatTime (bestTime)+"</color>";



		} else {
			timeElapsed += Time.deltaTime;
			scoreText.text = "Time: " + FormatTime(timeElapsed);
		}

	}

	void PlayerKilled(){
		spawner.active = false;
		var playerDestroyScript = player.GetComponent<DestroyOffScreen> ();
		playerDestroyScript.DestroyCallBack -= PlayerKilled ;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		timeManager.ManipulateTime (0, 5.5f);
		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";
		if (timeElapsed > bestTime) {
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);
			beatBestTime = true;
		}

	}



	void ResetGame(){
		spawner.active = true;
		
		player = GameObjectUtil.Instantiate (playerPrefab, new Vector3 (0, (Screen.height / PerfectCamera.pixelsToUnit) / 2, 0));

		var playerDestroyScript = player.GetComponent<DestroyOffScreen> ();
		playerDestroyScript.DestroyCallBack += PlayerKilled ;

		gameStarted = true;

		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0;
		beatBestTime = false;
	}


	string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);
		return string.Format ("{0:D2}:{1:D2}", t.Minutes,t.Seconds);

	}




}
