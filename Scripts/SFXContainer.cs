using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXContainer : MonoBehaviour {


	public AudioClip[] boom = new AudioClip[3];
	public AudioClip hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSound(int sound) { // TODO!
		GetComponent<AudioSource> ().Play ();

	}
}
