using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float velocity = GameObject.Find ("Player").GetComponent<Rigidbody2D> ().velocity.magnitude;



		gameObject.GetComponent<AudioSource> ().pitch = Mathf.Lerp (velocity / 25f, gameObject.GetComponent<AudioSource> ().pitch, 0.2f);
	}
}
