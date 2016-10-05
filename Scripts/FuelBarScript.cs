using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float fuel = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ().fuel / 100000f;

		Color fillColor = new Color (1 - fuel, fuel, 0);
		GetComponent<Image> ().color = fillColor;

	}
}
