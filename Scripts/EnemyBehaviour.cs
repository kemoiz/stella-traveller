using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


	public static float raycastDistance = 45;

	public bool respawn = false;

	public bool playerVisible = false, isShooting = false, toDelete = false;

	public float shootTimeout = 1, deleteTimeout = -1;
	float maxShootTimeout = 1;

	public float speed = 0.1f;
	public float health = 100;
	float maximumSpeed = 1;

	Vector2 desiredMomentum = new Vector2(0,0);

	float _shotCounter = 0;
	List<PathFind.Point> path = null;

	public GameObject healthBar, blinkyTemplate, bullet;




	// Use this for initialization
	void Start () {

		//GetComponent<AudioSource> ().clip = GetComponent<SFXContainer> ().boom [(int) Random.Range (0, 2)];

	}


	void Update() {
		Vector2 playerPos = GameObject.Find ("Player").transform.position;
		shootTimeout += 0.05f;
		maxShootTimeout = Mathf.Sin (Time.frameCount / 10f) + 1;

		if (shootTimeout >= maxShootTimeout && isShooting && playerVisible && !toDelete) {
			GameObject projectile = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
 
			playerPos.x += Random.Range (-1f, 1f) + GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x;
			playerPos.y += Random.Range (-1f, 1f) + GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y;

			projectile.GetComponent<Rigidbody2D> ().AddForce ((  playerPos - new Vector2(projectile.transform.position.x, projectile.transform.position.y)).normalized * 5000);
			projectile.GetComponent<BlinkysBallScript> ().shot = true;
			Physics2D.IgnoreCollision (projectile.GetComponent<Collider2D> (), GetComponent<Collider2D> ());

			shootTimeout = 0;
		}




		_shotCounter -= 0.02f;

		if (_shotCounter > 0 && !toDelete) {
			GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (health != 100) {
			transform.FindChild ("health_bar").gameObject.SetActive (true);
			healthBar.transform.localScale = new Vector2 (health / 100f, 1);

		}
		else
			transform.FindChild ("health_bar").gameObject.SetActive (false);
		
		

		if (toDelete) {
			health = 0;
			GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, deleteTimeout - 1);
			deleteTimeout -= 0.01f;

		}

		if (toDelete && deleteTimeout <= 0) {
			Destroy (this.gameObject);
		}


		if (health <= 0 && !toDelete) {

			if (respawn) {
				GameObject blinky = Instantiate (this.gameObject, this.transform.position, this.transform.rotation) as GameObject;
				Vector2 enemySpawn = new Vector2 (0, 0);
				do {
					float randx = Random.Range (15, 40), randy = Random.Range (15, 40);

					if (Random.value > 0.75f) {
						enemySpawn = new Vector2 (playerPos.x + randx, playerPos.y + randy);
					} else if (Random.value > 0.5f) {
						enemySpawn = new Vector2 (playerPos.x - randx, playerPos.y + randy);
					} else if (Random.value > 0.25f) {
						enemySpawn = new Vector2 (playerPos.x + randx, playerPos.y - randy);
					} else {
						enemySpawn = new Vector2 (playerPos.x - randx, playerPos.y - randy);
					}


				} while (Physics2D.OverlapPoint (enemySpawn));

				blinky.transform.position = enemySpawn;
				blinky.GetComponent<EnemyBehaviour> ().health = 100;
			}
			toDelete = true;
			GameState.score += 1;


			GetComponent<AudioSource> ().Play ();
			deleteTimeout = 2;
 
			GetComponent<PolygonCollider2D> ().enabled = false;
			transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.clear;



		}



	}


	// Update is called once per frame
	void FixedUpdate () {
		float dist = Vector2.Distance (transform.position, GameObject.Find ("Player").transform.position);
		GetComponent<Rigidbody2D> ().rotation = Mathf.Lerp (GetComponent<Rigidbody2D> ().rotation, 0, 0.8f);
		Vector2 playerPos = GameObject.Find ("Player").transform.position;

		RaycastHit2D hit = Physics2D.Raycast (transform.position, GameObject.Find ("Player").transform.position - transform.position, raycastDistance);
		 //Debug.Log ("ray distance:" + hit.distance);

		if (hit.collider != null && hit.transform.CompareTag ("Player")) {
			playerVisible = true;
		} else {
			playerVisible = false;
		}

		if (dist < 50) {
		//	playerVisible = true;

		}
		if (dist < 15) {
			isShooting = true;
		} else {
			isShooting = false;
		}


		if (playerVisible)
			CatchPlayer (playerPos);
		else
			WalkAround ();
			//
			//		
	


	}

	void WalkAround() {



		//AlternativeCatchPlayer (playerPos);

	}
		

	void AlternativeCatchPlayer () {
		Vector2 pos = transform.position;
		desiredMomentum = new Vector2 (0, 0);

		PathFind.Point _from = new PathFind.Point (50, 50);
		PathFind.Point _to = new PathFind.Point (Random.Range(0, 100), Random.Range(0, 100));

		if (Time.frameCount % 20 == 0) {
			bool[,] tilesmap = new bool[100, 100];
			for (int x = 0; x <= 99; x ++) {
				for (int y = 0; y <= 99; y++) {

					tilesmap [x, y] = !Physics2D.OverlapPoint (new Vector2 (x, y) + pos);
					if (Input.GetKey (KeyCode.I) && !tilesmap[x,y]) {
						Debug.Log (tilesmap [x, y] + "checking x:" + (x+pos.x) + " y:" + (y+pos.y));
					}
				}
			}

			PathFind.Grid grid = new PathFind.Grid (100, 100, tilesmap);







			path = PathFind.Pathfinding.FindPath (grid, _from, _to);
		}
		if (path != null && Time.frameCount % 20 < path.ToArray().Length) {
			desiredMomentum.x = (path [Time.frameCount % 20].x   - 50) * speed;
			desiredMomentum.y = (path [Time.frameCount % 20].y   - 50) * speed;
		}

		GetComponent<Rigidbody2D>().AddForce(desiredMomentum);
	}

	void CatchPlayer(Vector2 playerPos) {

		Vector2 pos = transform.position;
		desiredMomentum = new Vector2 (0, 0);

		if (pos.x - playerPos.x < 0) { 
			desiredMomentum.x += speed;
		} 
		if (pos.x - playerPos.x > 0) { 
			desiredMomentum.x -= speed;
		} 
		if (pos.y - playerPos.y < 0) { 
			desiredMomentum.y += speed;
		} 
		if (pos.y - playerPos.y > 0) { 
			desiredMomentum.y -= speed;
		} 


		
		//transform.position = pos;
		if (_shotCounter > 0) {
			GetComponent<Rigidbody2D> ().AddForce (desiredMomentum * 13f * speed); // blinky gets angry then
		} else {
			GetComponent<Rigidbody2D> ().AddForce (desiredMomentum * 3.5f * speed);
		}


		//transform.position = Vector2.Lerp (transform.position, pos, 0.7f);


	}

	void Shot() {
		float dist = Vector2.Distance (transform.position, GameObject.Find ("Player").transform.position);

		health -= (25 - dist);

		Debug.Log("OUCH!");
		_shotCounter = 1;
	}
}
