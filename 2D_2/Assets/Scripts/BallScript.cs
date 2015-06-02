using UnityEngine;
using System.Collections;

public class BallScrypt : MonoBehaviour
{
	private bool ballIsActive;
	private Vector3 ballPosition;
	private float ballStartPositionY;
	public Vector2 ballInitialForce = new Vector2 (100.0f, 300.0f);
	public GameObject playerObject;


	// Use this for initialization
	void Start ()
	{
		ballIsActive = false;
		ballStartPositionY = gameObject.transform.position.y;
		ballPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Jump") == true) {
			if (!ballIsActive) {
				GetComponent<Rigidbody2D> ().AddForce (ballInitialForce);
				ballIsActive = true;
			}
		}

		if (!ballIsActive && playerObject != null) {
			ballPosition.x = playerObject.transform.position.x;
			gameObject.transform.position = ballPosition;
		}

		if (ballIsActive && gameObject.transform.position.y != playerObject.transform.position.y) {
			ballIsActive = false;
			ballPosition.x = playerObject.transform.position.x;
			ballPosition.y = ballStartPositionY;
			gameObject.transform.position = ballPosition;
		}


	}
}
