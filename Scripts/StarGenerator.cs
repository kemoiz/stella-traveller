using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {
	Texture2D _t;

	public int starCount;

	public bool did = false;
	GameObject starfieldSprite;

	public GameObject starPrefab;

	public bool halvedFramerate = false;


    Star[] stars;
	// Use this for initialization
	void Start () {
		GenerateStars (starCount);
	}

	void GenerateStars (int starCount) {
		_t = new Texture2D (320, 200);
//		_t.alphaIsTransparency = true;
		_t.filterMode = FilterMode.Point;
        // clearing bkg
		Color[] cols = _t.GetPixels();
		stars = new Star[starCount];
		for (int i =0; i <starCount; i++) {

 

		}
		for( int i = 0; i < cols.Length; ++i ) {
			cols [i].a = 0;
		}
		_t.SetPixels( cols);

		for (int i = 0; i < starCount; i++) {
			stars [i] = new Star ();
			stars [i].star = Instantiate (starPrefab, this.transform);
			float x =  Random.Range (0, 30);
			float y =  Random.Range (0, 20);
			Color color = new Color (Random.Range (0.8f, 1f), Random.Range (0.8f, 1f), Random.Range (0, 1f));
			stars [i].x = x;
			stars [i].y = y;
			stars [i].depth = Random.Range (0.3f,0.6f);
			stars [i].depthY = Random.Range (0.3f,0.6f) + stars[i].depth;
			stars [i].initialMagicRandomValue = Random.Range (0, 20);
			stars [i].star.GetComponent<SpriteRenderer> ().color = new Color (stars [i].depth, stars [i].depth, stars [i].depth);
			stars [i].star.transform.localPosition = new Vector3 (x, y, 0);
			stars [i].star.transform.localScale = new Vector3 (stars [i].depth * starPrefab.transform.localScale.x, stars [i].depth * starPrefab.transform.localScale.y, 1);
		}
		//_t.Apply ();
 
		starfieldSprite = new GameObject ("Starfield Sprite");
		starfieldSprite.AddComponent<SpriteRenderer> ();
		Sprite _s = Sprite.Create (_t, Rect.MinMaxRect (0, 0, 320, 200), new Vector2 (0, 0), 10);
	 
		starfieldSprite.GetComponent<SpriteRenderer> ().sprite = _s;
		starfieldSprite.GetComponent<SpriteRenderer> ().sortingOrder = -1000;

		starfieldSprite.GetComponent<SpriteRenderer> ().material = this.GetComponent<SpriteRenderer> ().material;



		starfieldSprite.transform.parent = this.transform.parent;
		starfieldSprite.transform.position = this.transform.position;
		starfieldSprite.transform.localScale = this.transform.localScale;


	}
	// Update is called once per frame
	void Update () {
		float x, y;
		Vector2 cameraPos = GameObject.FindGameObjectWithTag ("MainCamera").transform.position;

	//	if (halvedFramerate && Time.frameCount % 2 == 0)
	//		return;
		

		for (int i = 0; i < starCount; i++) {
			//stars [i] = new Star ();
			//Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.position.x);
			x =   stars [i].depth * (cameraPos.x / 10f);
		    y =   stars [i].depthY * (cameraPos.y / 10f);

			 
			stars [i].x =((x * 10) % 30);
			stars [i].y =((y * 10) % 20);
 

			stars [i].star.transform.localPosition = new Vector2 (- stars [i].x, 20-stars [i].y);


		}
 
	
 
	
	}

	private class Star {

		public float x, y, depth, depthY, initialMagicRandomValue;
 
		public GameObject star;


	}
}
