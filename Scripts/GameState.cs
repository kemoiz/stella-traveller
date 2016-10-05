using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Utility;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

	public bool isAlive, inDialogue, isRefueling, isControlable; // na razie tyle xd

	public GameObject currentDialogueTarget;

	float splashTime = 1f;

	public Texture2D dialogueOverlay, refuelingOverlay;
	// Use this for initialization
	void Start () {
		isAlive = true;
		inDialogue = false;
		isRefueling = false;
		isControlable = true;
		GameObject.Find ("SplashScreen").SetActive (true);

		
	}
	
	// Update is called once per frame
	void Update () {



		if (splashTime > 0) {
			splashTime -= 0.01f * Time.deltaTime * 60;
			GameObject.Find ("SplashScreen").GetComponent<Image> ().color = new Color (1, 1, 1, splashTime);
		}
		if (splashTime <= 0 && GameObject.Find ("SplashScreen").activeSelf) {
			GameObject.Find ("SplashScreen").transform.position = new Vector2 (99999, 99999); //hahaha

		}

 
		if (inDialogue && GameObject.Find("Main Camera").GetComponent<ScreenOverlay>().texture.name != "dialogue overlay") {

			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().texture = dialogueOverlay;

 
		}

		if (inDialogue) {
			GameObject.Find ("Main Camera").GetComponent<Camera2DFollow> ().target = currentDialogueTarget.transform;

		} else {

			GameObject.Find ("Main Camera").GetComponent<Camera2DFollow> ().target = GameObject.Find ("Player").transform;
		}
		if (isRefueling && GameObject.Find("Main Camera").GetComponent<ScreenOverlay>().texture.name != "refueling overlay") {

			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().texture = refuelingOverlay;

		}

		if (inDialogue || !isAlive)
			isControlable = false;
		if (!inDialogue && isAlive)
			isControlable = true;

	}
}
