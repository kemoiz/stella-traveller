using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HiScore : MonoBehaviour {


	public int score = GameState.score;


	// Use this for initialization
	void Start () {
		GameObject.Find ("HighScore").GetComponent<Text> ().text = score.ToString ();
		StartCoroutine (LoadGameRoutine ());

	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find ("HighScore").GetComponent<Text> ().text =  score.ToString ();


	}

	private IEnumerator LoadGameRoutine()
	{
 
		AsyncOperation ao = SceneManager.LoadSceneAsync("scene");
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			float progress = Mathf.Clamp01(ao.progress / 0.9f);

			Debug.Log (progress);


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
}
