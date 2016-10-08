using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class BlockDestroyer : MonoBehaviour {

	GameObject[] blocks;
	public Sprite blockSprite;

	void Start () {
		blocks = GameObject.FindGameObjectsWithTag ("Blocks");

		foreach (GameObject block in blocks) {
			block.AddComponent<SpriteRenderer> ();

			block.GetComponent<SpriteRenderer> ().sprite = blockSprite;

		}
	}
	
	// Update is called once per frame
	void Update () {
      
	}
}
