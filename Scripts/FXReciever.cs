using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
public class FXReciever : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<ScreenOverlay> ().intensity < 0.2f) {
			gameObject.GetComponent<ScreenOverlay> ().intensity = 0;
			GameObject.Find ("RefuelingText").GetComponent<Text> ().color = new Color (1, 1, 1, 0);
			gameObject.GetComponent<ScreenOverlay> ().enabled = false;
			GameObject.Find ("MinimapCamera").GetComponent<NoiseAndGrain>().enabled =false;
 
		} else {
			gameObject.GetComponent<ScreenOverlay> ().enabled = true; // Screen overlaying doesn't work with minimap on most Android devices, so let's turn it off 
				                                                      // when not neccessary.
			GameObject.Find ("MinimapCamera").GetComponent<NoiseAndGrain>().enabled = true;
 

		}
	}

	void SetOverlay(float strength) {

		gameObject.GetComponent<ScreenOverlay> ().intensity = strength;

	}
}
