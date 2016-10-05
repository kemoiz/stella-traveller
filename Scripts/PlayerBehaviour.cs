using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour {


	public int fuel = 100000;
	public int fuelUsage = 1; //per frame
	public int chargingSpeed = 71;

	public float shootTimeout = 1;
	float maxShootTimeout = 0.6f;
	//public Transform bullet;
	public GameObject bullet2;
	public Transform leftBulletHelper, rightBulletHelper;

	public bool docked = false;
	bool isShooting = false;
	bool isMobile = Application.isMobilePlatform;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void FixedUpdate() {

		if (!isMobile)
			isShooting = Input.GetKey (KeyCode.L);
		
		

		shootTimeout += 0.05f;
		if (shootTimeout >= maxShootTimeout && isShooting) {
			GameObject leftProjectile = Instantiate (bullet2, leftBulletHelper.transform.position, leftBulletHelper.transform.rotation) as GameObject;
			GameObject rightProjectile = Instantiate (bullet2, rightBulletHelper.transform.position, rightBulletHelper.transform.rotation) as GameObject;

			leftProjectile.GetComponent<Rigidbody2D> ().AddForce (leftProjectile.transform.right * 1000);
			leftProjectile.GetComponent<BulletBehaviour> ().shoot = true;
			Physics2D.IgnoreCollision (leftProjectile.GetComponent<Collider2D> (), GetComponent<Collider2D> ());


			rightProjectile.GetComponent<Rigidbody2D> ().AddForce (rightProjectile.transform.right * 1000);
			rightProjectile.GetComponent<BulletBehaviour> ().shoot = true;

			GetComponent<AudioSource> ().Play ();


			Physics2D.IgnoreCollision (rightProjectile.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			shootTimeout = 0;
		}
	}

	void Update () {
		//Camera.current.orthographicSize = Mathf.Lerp(Camera.current.orthographicSize, 9 + (GetComponent<Rigidbody2D> ().velocity.magnitude / 3), 0.1f);
		float emissionRate = this.GetComponent<Rigidbody2D> ().velocity.magnitude * 5;
		if (emissionRate < 5)
			emissionRate = 0;
		SetEmissionRate (this.GetComponentInChildren<ParticleSystem> (), emissionRate);
	


		Debug.Log ("velocity: " + GetComponent<Rigidbody2D> ().velocity.magnitude);

		fuel -= (int) this.GetComponent<Rigidbody2D> ().velocity.magnitude * fuelUsage;
		GameObject.Find ("Slider").GetComponent<Slider> ().value = fuel;

		int dockCount = GameObject.FindGameObjectsWithTag ("Dock").Length;
		if (Time.frameCount % 10 == 0) {
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Dock")) {

				if (Vector3.Distance (this.transform.position, obj.transform.position) > obj.GetComponent<PadLogic> ().pullDistance) {
					dockCount--;
 
				}
			 
			}
		}
	 

			GameObject.Find ("GameState").GetComponent<GameState> ().isRefueling = docked;

	 

		if (fuel < 0)
			fuel = 0;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Dock" && fuel < 100000)
			docked = true;
		if (coll.gameObject.tag == "Projectile") {
			fuel -= 5000;

		}

	}
	void OnCollisionExit2D(Collision2D coll) {
		docked = false;
	}

	public void Refuel() {
		
 
			fuel += chargingSpeed;

		if (fuel > 100000)
			fuel = 100000;

		

	}

   void SetEmissionRate(ParticleSystem particleSystem, float emissionRate)
	{
		var emission = particleSystem.emission;
		var rate = emission.rate;
		rate.constantMax = emissionRate;
		emission.rate = rate;
	}

	public void Brake () {

		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;


	}

	public void StartShooting() {

		isShooting = true;

		
	}
	public void StopShooting() {

		isShooting = false;


	}
}
