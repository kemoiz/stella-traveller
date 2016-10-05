using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CoordinateText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int playerX = (int) GameObject.FindGameObjectWithTag ("Player").transform.position.x;
		int playerY = (int) GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		int fuel = (int) GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour>().fuel;

		if (Time.frameCount % 5 == 0)
		gameObject.GetComponent<Text> ().text = "X:" + playerX + "  Y:" + playerY + " fuel:" + fuel + " fps:" + ((int) (1f / Time.deltaTime));


	}
}
