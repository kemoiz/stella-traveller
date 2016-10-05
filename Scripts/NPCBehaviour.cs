using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
public class NPCBehaviour : MonoBehaviour {

	List<PathFind.Point> path;
	Vector2 preparedEnemyPos;
	// Use this for initialization
	Vector2 initialPos;
	float intensityCounter = 0;
	bool threwDialogue = false;
	string tutorialString1 = "Hi there!\nUse docks to refuel ship and watch out for Blinkies. \n Have fun and don't get pissed by bugs!";

	public GameObject dialogueText;



	void Start () {
		//PreparePath ();
		initialPos = this.transform.position;
 


	}
	
	// Update is called once per frame
	void Update () {

		Vector2 playerPos = GameObject.Find ("Player").transform.position;



		if (Vector2.Distance (playerPos, transform.position) <= 5f && !GameObject.Find ("GameState").GetComponent<GameState> ().inDialogue && !threwDialogue) {
			ShowDialogue ();
		} 
		bool readyToForward = true;
		if (dialogueText.GetComponent<DialogueHandler> ().toView != null) {
		    readyToForward = dialogueText.GetComponent<DialogueHandler> ().counter * 3 > dialogueText.GetComponent<DialogueHandler> ().toView.Length;
		} 
 

		if (Input.touchCount > 0 && readyToForward) {
			Debug.Log ("touched");
			CloseDialogue ();
			threwDialogue = true;

		}

		if (Vector2.Distance (playerPos, transform.position) > 5.5f) {
			CloseDialogue ();
			threwDialogue = false;
		}

		FrameMovement ();



		if (GameObject.Find ("GameState").GetComponent<GameState> ().inDialogue) {

			if (intensityCounter < 1f) 
				intensityCounter += 0.02f;
			

			GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = intensityCounter;

		} else {

			if (intensityCounter > 0) {
				intensityCounter -= 0.02f;
				GameObject.Find ("Main Camera").GetComponent<ScreenOverlay> ().intensity = intensityCounter;
			}

		}

		GameObject.Find ("DialogueBox").transform.localScale = new Vector2 (12, intensityCounter);
		if (GameObject.Find ("DialogueBox").transform.localScale.y < 0.05f) 
			GameObject.Find ("DialogueBox").transform.localScale = new Vector2 (0, 0); 
		// i'm surely going to hell for those ugly workarounds

	}

	void FrameMovement () {


		transform.position = new Vector2 (initialPos.x, initialPos.y + Mathf.Sin (Time.frameCount / 20f) * 0.3f);
	}

	void ShowDialogue () { 
		GameObject.Find ("GameState").GetComponent<GameState> ().inDialogue = true;
		GameObject.Find ("DialogueText").GetComponent<DialogueHandler> ().ThrowDialogue (tutorialString1);
		GameObject.Find ("GameState").GetComponent<GameState> ().currentDialogueTarget = this.gameObject;
		GameObject.Find ("Player").GetComponent<PlayerBehaviour> ().Brake ();

		intensityCounter = 0;

	}

	void CloseDialogue () {

		GameObject.Find ("GameState").GetComponent<GameState> ().inDialogue = false;
		GameObject.Find ("DialogueText").GetComponent<DialogueHandler> ().FinishDialogue ();
	}

	void PreparePath() {
		bool[,] tilesmap = new bool[100, 100];
		//prepare obstaclemap

		preparedEnemyPos = this.transform.position;
		for (int x = 0; x < 100; x++) {
			for (int y = 0; y < 100; y++) {

				tilesmap [x, y] = !Physics2D.OverlapBox (new Vector2 (x, y) + preparedEnemyPos, new Vector2 (1, 1), 0);
				//Debug.Log (tilesmap [x, y]);
			}
		}


		PathFind.Grid grid = new PathFind.Grid(100, 100, tilesmap);
		// create source and target points
		PathFind.Point _from = new PathFind.Point(50, 50);
		PathFind.Point _to = new PathFind.Point((int) (GameObject.Find("Player").transform.position.x - preparedEnemyPos.x),
			(int) (GameObject.Find("Player").transform.position.y - preparedEnemyPos.y));

		// get path
		// path will either be a list of Points (x, y), or an empty list if no path is found.
        path = PathFind.Pathfinding.FindPath(grid, _from, _to);
		Debug.Log (path.ToArray ().Length);
		foreach (PathFind.Point point in path) {

			Debug.Log ("path x: " + point.x + " y: " + point.y);

		}

	}
}
