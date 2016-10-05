using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	// Use this for initialization

	public bool  shoot = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = GameObject.Find ("Player").transform.position;
		float distance = Vector3.Distance (this.transform.position, playerPos);

		if (distance > 20 && shoot)
			Destroy (this.gameObject);


	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.CompareTag ("Enemy")) {

			coll.gameObject.SendMessage ("Shot");


		}

		Destroy (this.gameObject);

	}
}
