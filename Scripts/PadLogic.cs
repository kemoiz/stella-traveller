using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class PadLogic : MonoBehaviour {

	public float pullStrength = 0.1f;
	public float pullDistance = 2f;


	// Use this for initialization
	void Start () {
 
	}

	// Update is called once per frame
	void Update () {

		Vector3 playerPos = GameObject.Find ("Player").transform.position;
		float distance = Vector3.Distance (this.transform.position, playerPos);
		//Debug.Log (distance);
		if (distance > 2 && distance < pullDistance) {

			GameObject.Find ("Player").GetComponent<Rigidbody2D> ().AddForce ((transform.position -playerPos).normalized * pullStrength, ForceMode2D.Impulse);
		}
 
		if (distance < pullDistance) {

			GameObject.Find ("GameState").GetComponent<GameState> ().isRefueling = true;
			Debug.Log ("pulling!!!");
			GameObject.Find ("Player").GetComponent<PlayerBehaviour>().Refuel();
			//GameObject.Find ("Player").GetComponent<Rigidbody2D> ().
			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = (pullDistance - distance) * .75f;
			GameObject.Find ("RefuelingText").GetComponent<Text> ().color = new Color (1, 1, 1, GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity);
		} else {


		//	GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = 0;
		//	GameObject.Find ("RefuelingText").GetComponent<Text> ().color = new Color (1, 1, 1, 0);
		}

	}

	public void setOverlay() {
		Vector3 playerPos = GameObject.Find ("Player").transform.position;
		float distance = Vector3.Distance (this.transform.position, playerPos);
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().AddForce ((transform.position - playerPos).normalized * pullStrength, ForceMode2D.Impulse);
		GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = (pullDistance - distance) * .75f;
		GameObject.Find ("RefuelingText").GetComponent<Text> ().color = new Color (1, 1, 1, GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity);
	}
	public void offOverlay() {
		GameObject.Find ("GameState").GetComponent<GameState> ().isRefueling = false;
		GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = 0;
		GameObject.Find ("RefuelingText").GetComponent<Text> ().color = new Color (1, 1, 1, 0);

	}
}
