using UnityEngine;
using System.Collections;
using CnControls;
public class ControllerScript : MonoBehaviour {

	public float speed = 25f;
	public float verticalSpeed = 20f;
	public Vector2 sideEnginePosition = new Vector2 (0, -1);
	static bool isClicked = false;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	 

	void FixedUpdate () {

		if (Input.GetKeyDown (KeyCode.L) || Input.touchCount > 1) {
		//	gameObject.GetComponent<PlayerBehaviour> ().Shoot ();
		}

	
		if (Input.GetKey (KeyCode.Space) || isClicked) {

			gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, speed * 60 * Time.deltaTime));

		}
		Vector2 worldForcePosition = transform.TransformPoint (sideEnginePosition);
		if (Input.GetKey (KeyCode.A)) {
			gameObject.GetComponent<Rigidbody2D> ().AddForceAtPosition (transform.right * -verticalSpeed, worldForcePosition);

		}
		if (Input.GetKey (KeyCode.D)) {
			gameObject.GetComponent<Rigidbody2D> ().AddForceAtPosition (transform.right * verticalSpeed, worldForcePosition);

		}
		float joyX = CnInputManager.GetAxis ("Horizontal");
		float joyY = CnInputManager.GetAxis ("Vertical");
	
		;
		if (Mathf.Abs (Mathf.Rad2Deg * (Mathf.Sin (joyX / joyY))) > 0.01f && GameObject.Find("GameState").GetComponent<GameState>().isControlable) {

			float desiredRotation = Mathf.Atan2 (-joyY, -joyX) * Mathf.Rad2Deg - 90;
			Debug.Log ("Desired Rotation:" + desiredRotation + "Rigidbody rotation:" + gameObject.GetComponent<Rigidbody2D> ().rotation);
 
			if (gameObject.GetComponent<Rigidbody2D> ().rotation > 75) {
				gameObject.GetComponent<Rigidbody2D> ().rotation = desiredRotation;
			} 
			if (gameObject.GetComponent<Rigidbody2D> ().rotation < -255) {

				gameObject.GetComponent<Rigidbody2D> ().rotation = desiredRotation;
			}
			 
			if (desiredRotation < -265) {
				desiredRotation += 360;
			}

			if (gameObject.GetComponent<Rigidbody2D> ().rotation < -200 && desiredRotation > 65) {
				gameObject.GetComponent<Rigidbody2D> ().MoveRotation (gameObject.GetComponent<Rigidbody2D> ().rotation - 360);
				gameObject.GetComponent<Rigidbody2D> ().rotation = desiredRotation;			
			}
			if (gameObject.GetComponent<Rigidbody2D> ().rotation > 65 && desiredRotation < -200) {
				gameObject.GetComponent<Rigidbody2D> ().MoveRotation (gameObject.GetComponent<Rigidbody2D> ().rotation + 360);
				gameObject.GetComponent<Rigidbody2D> ().rotation = desiredRotation;
			}

				gameObject.GetComponent<Rigidbody2D> ().rotation = Mathf.SmoothStep (gameObject.GetComponent<Rigidbody2D> ().rotation, desiredRotation, 0.2f);
	
			gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, Mathf.Max (Mathf.Abs(joyX), Mathf.Abs(joyY))) * 60 * Time.deltaTime * speed);
		

			//gameObject.GetComponent<Rigidbody2D> ().AddForceAtPosition (transform.right * -verticalSpeed * CnInputManager.GetAxis("Horizontal"), worldForcePosition);
			//
			//if (CnInputManager.GetAxis("Vertical") < 0)
			//gameObject.GetComponent<Rigidbody2D> ().AddForce (-transform.up * speed * 60 * Time.deltaTime * CnInputManager.GetAxis("Vertical"));
			//gameObject.GetComponent<Rigidbody2D> ().AddTorque
 	
		}
	}
}
