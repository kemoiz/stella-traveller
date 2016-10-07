using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Utility;
using UnityStandardAssets._2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameState : MonoBehaviour {

	public bool isAlive, inDialogue, isRefueling, isControlable; // na razie tyle xd

	public GameObject currentDialogueTarget;

	float splashTime = 1f;
	public static int score = 0;

	public Texture2D dialogueOverlay, refuelingOverlay;
	// Use this for initialization
	void Start () {
		isAlive = true;
		inDialogue = false;
		isRefueling = false;
		isControlable = true;
		GameObject.Find ("SplashScreen").SetActive (true);

		if (!Application.isMobilePlatform) {
			GameObject.Find ("ShotButton").SetActive (false);
			GameObject.Find ("ShotButton_ovl").SetActive (false);
			GameObject.Find ("Joystick").SetActive (false);
		}

		
	}
	
	// Update is called once per frame
	void Update () {



		if (splashTime > 0) {
			splashTime -= 0.01f * Time.deltaTime * 60;
			GameObject.Find ("SplashScreen").GetComponent<Image> ().color = new Color (1, 1, 1, splashTime);
		}
		if (splashTime <= 0 && GameObject.Find ("SplashScreen").activeSelf) {
			GameObject.Find ("SplashScreen").transform.position = new Vector2 (99999, 99999); //hahaha

		}

 
		if (inDialogue && GameObject.Find("Main Camera").GetComponent<ScreenOverlay>().texture.name != "dialogue overlay") {

			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().texture = dialogueOverlay;

 
		}

		if (inDialogue) {
			GameObject.Find ("Main Camera").GetComponent<Camera2DFollow> ().target = currentDialogueTarget.transform;

		} else {

			GameObject.Find ("Main Camera").GetComponent<Camera2DFollow> ().target = GameObject.Find ("Player").transform;
		}
		if (isRefueling && GameObject.Find("Main Camera").GetComponent<ScreenOverlay>().texture.name != "refueling overlay") {

			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().texture = refuelingOverlay;

		}

		if (inDialogue || !isAlive)
			isControlable = false;
		if (!inDialogue && isAlive)
			isControlable = true;

		if (GameObject.Find ("Player").GetComponent<PlayerBehaviour> ().fuel <= 0) {
			Scene scene = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);

			//StartCoroutine(LoadGameRoutine ());
		}

	}


	private IEnumerator LoadGameRoutine()
	{

		AsyncOperation ao = SceneManager.LoadSceneAsync("hiscore");
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			float progress = Mathf.Clamp01(ao.progress / 0.9f);



			// Progress ends at exactly 0.9
			//   Progress of 1.0 is reserved for scene activation
			if (ao.progress == 0.9f)
			{
				Debug.Log("Loading complete");
			 
				ao.allowSceneActivation = true;
			}

			yield return null;
		}

 
	}
}
