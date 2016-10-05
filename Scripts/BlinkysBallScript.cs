using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkysBallScript : MonoBehaviour {
	
	float lifetime = 0;
	static float maxLifetime = 5;
	public bool shot = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifetime += 0.01f;
		if (lifetime > maxLifetime && shot)
			Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Projectile") 
	 Destroy (this.gameObject);

	}
}
