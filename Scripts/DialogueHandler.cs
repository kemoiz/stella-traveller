using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueHandler : MonoBehaviour {

	public string toView = null;
	public int typingSpeed = 3; // amount of frames to next char
	string currentText = null;
	public int counter = 0;

	void Start () {
		
	}


	void Update () {



		if (currentText != null && !GameObject.Find ("GameState").GetComponent<GameState> ().inDialogue && ( 2* Time.frameCount) % typingSpeed == 0) {


			if (currentText.Length > 0) 
				currentText = currentText.Remove (currentText.Length - 1);
			GetComponent<Text> ().text = currentText;


		}

		if (toView != null && Time.frameCount % typingSpeed == 0) {
         
			if (counter > 0 && counter < toView.Length)
				currentText = toView.Remove (counter);
			else
				currentText = toView;
			GetComponent<Text> ().text = currentText;

			if (!currentText.EndsWith (" "))
				
				transform.parent.parent.GetComponent<AudioSource> ().Play ();
			currentText = currentText.Remove(currentText.Length -1, 1) + (char) Random.Range(0,255);
			counter++;
			if (counter-1 == toView.Length) {
				toView = null;
				counter = 1;
			}
		}

	}

	public void ThrowDialogue (string text) {
		toView = text;
		currentText = null;
		counter = 1;
	}
	public void FinishDialogue() {
		toView = null;

	}
}
