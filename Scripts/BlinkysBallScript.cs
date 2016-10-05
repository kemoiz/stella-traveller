using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkysBallScript : MonoBehaviour {
	
	float lifetime = 0;
	static float maxLifetime = 5;
	public bool shot = false;
	public int health =4 ;

	public Sprite d1, d2, d3;

	bool bounce = false;
	Vector2 recoverVel;
	// Use this for initialization
	void Start () {
		
	}


	void FixedUpdate() {
		if (bounce) {
			GetComponent<Rigidbody2D> ().velocity = recoverVel; // xD guess what it does
			bounce = false;
		}

	}
	// Update is called once per frame
	void Update () {
		lifetime += 0.01f;
		if (lifetime > maxLifetime && shot )
			Destroy (this.gameObject);
		if (health <= 0)
			Destroy (this.gameObject);

		if (lifetime >= 4.90) {

			//health = (int) (4 - (lifetime - 4.90)) * 40;

		}
		switch (health) {
		case 3:
			GetComponent<SpriteRenderer> ().sprite = d1;
			break;
		case 2:
			GetComponent<SpriteRenderer> ().sprite = d2;
			break;
		case 1:
			GetComponent<SpriteRenderer> ().sprite = d3;
			break;


		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") 
	 Destroy (this.gameObject);

		if (coll.gameObject.tag == "Projectile"){
			health -= 1;
			Physics2D.IgnoreCollision (GetComponent<Collider2D>(), coll.collider);
			recoverVel = GetComponent<Rigidbody2D> ().GetPointVelocity (transform.position);

			bounce = true;
		}
		

	}
}
