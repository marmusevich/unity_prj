using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
	public float maxSpeedX = 10.0f;
	public float maxSpeedY = 10.0f;

	public Bounds bg;
	public float  my_X_min;
	public float  my_X_max;

	public float  bg_X_min;
	public float  bg_X_max;


	public GameObject asteroid;

//	public 
	float moveX;
	//public 
	float moveY;

	public int health = 100;

	// Use this for initialization
	void Start ()
	{
		Start1 ();
	}


	void Start1 ()
	{
		for (int y = 0; y < 5; y++) {
			for (int x = 0; x < 5; x++) {
				Instantiate (asteroid, new Vector3 (x, y / 2 + 5, 0), Quaternion.identity);
			}
		}
	}


	void FixedUpdate ()
	{
		moveX = Input.GetAxis ("Horizontal");
		moveY = Input.GetAxis ("Vertical");
	}


	// Update is called once per frame
	void Update ()
	{
		//Vector3 newPos = new Vector3 (moveX, moveY, transform.position.z);
		//transform.position = newPos;

		//Bounds 
		bg = GameObject.Find ("background").GetComponent<SpriteRenderer> ().sprite.bounds;
		Vector3 bgPos = GameObject.Find ("background").transform.position; 
		//Bounds 
		Vector3 myPos = transform.position; 
		Bounds my = GetComponent<SpriteRenderer> ().sprite.bounds; 


		my_X_min = my.min.x + myPos.x;
		my_X_max = my.max.x + myPos.x;

		bg_X_min = bg.min.x + bgPos.x;
		bg_X_max = bg.max.x + bgPos.x;

		if ((my_X_min <= bg_X_min) && moveX < 0)
		//if ((my.min.x + myPos.x <= bg.min.x + bgPos.x) || (my.max.x + myPos.x >= bg.max.x + bgPos.x))
			moveX = 0.0f;

		if ((my_X_max >= bg_X_max) && moveX > 0)
			//if ((my.min.x + myPos.x <= bg.min.x + bgPos.x) || (my.max.x + myPos.x >= bg.max.x + bgPos.x))
			moveX = 0.0f;



		//		if (my.min.y >= bg.min.y || my.min.y <= bg.min.y)
//			moveY = 0.0f;




		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveX * maxSpeedX, moveY * maxSpeedY);

		if (Input.GetKeyDown (KeyCode.R))
		  // Application.LoadLevel(Application.loadedLevel());
			Application.LoadLevel (Application.loadedLevelName);

		// game over - restart
		if (health <= 0) {
			Application.LoadLevel (Application.loadedLevelName);
			Destroy (gameObject);
		}
	}

	//по тагу для столкновения
	void OnCollisionEnter2D (Collision2D col)
	{
//		if (col.gameObject.tag == "enemy")
//		{
//			Destroy (col.gameObject);
//		}

//		if (col.gameObject.name == "GameObject") {
//			//Destroy (col.gameObject);
//			health --;
//		}

	}

	//по имени для тригеров (включено свойство тригер)
	void OnTriggerEnter2D (Collider2D col)
	{
		//if (col.gameObject.name == "Asteroid") 
		if (col.gameObject.tag == "enemy") {
			Destroy (col.gameObject);
			health -= col.gameObject.GetComponent< AsteroidController> ().health;
		}
	}

	void OnGUI ()
	{
		GUI.Box (new Rect (0, 0, 100, 100), "health: " + health);
	}

} // end class