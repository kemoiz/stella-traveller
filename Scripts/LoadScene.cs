﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadGameRoutine ());


	}
	
	// Update is called once per frame
	void Update () {
		 
	}




	private IEnumerator LoadGameRoutine()
	{
		Debug.Log("Loading start");
		AsyncOperation ao = SceneManager.LoadSceneAsync("scene");
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			GameObject.Find ("LoadingText").GetComponent<Text> ().text = "Loading progress: " + (int) (progress * 100) + "%";
 

			// Progress ends at exactly 0.9
			//   Progress of 1.0 is reserved for scene activation
			if (ao.progress == 0.9f)
			{
				Debug.Log("Loading complete");

				ao.allowSceneActivation = true;
			}

			yield return null;
		}

		Debug.Log("Scene activated");
	}
} 
