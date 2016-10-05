using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour {


	const float ceilValue = 1 / 30f;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioLowPassFilter> ().cutoffFrequency = 150;
	}

	// Update is called once per frame
	void Update () {
		float velocity = GameObject.Find ("Player").GetComponent<Rigidbody2D> ().velocity.magnitude * 400;



		gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = Mathf.Lerp(velocity, gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency, 0.65f) + 150;
	}

	float Ceil(float value) {
		return Mathf.Ceil (value / ceilValue) * ceilValue;
	}
}
