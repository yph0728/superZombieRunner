using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	// here pass in a time then use the duration as how many second of the first value
	public void ManipulateTime(float newTime, float duration){

		if (Time.timeScale == 0) {
			Time.timeScale = 0.1f;
		}

		StartCoroutine (FadeTo (newTime, duration));
	}



	IEnumerator FadeTo(float value , float time){

		for (float t = 0f; t < 1; t += Time.deltaTime/time) {

			//lerp alter a value  from a start position to an end position
			//over a certain period of time
			Time.timeScale = Mathf.Lerp(Time.timeScale, value,t);


			if(Mathf.Abs (value -Time.timeScale)< .01f){
				Time.timeScale = value;
				//return false;
				break;

			}

			yield return null;
		}

	}
}
