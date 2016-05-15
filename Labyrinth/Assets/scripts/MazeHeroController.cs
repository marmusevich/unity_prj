using UnityEngine;
using System.Collections;

public class MazeHeroController : MonoBehaviour
{
	public Transform CameraTransform;
	public Transform CodeStore;
	public float MaxSpeed = 10.0f;

	//private Rigidbody2D rigbody;
	private GameControler mGameControler = null;

	// Use this for initialization
	void Start()
	{
		//rigbody = GetComponent<Rigidbody2D>();
		mGameControler = CodeStore.GetComponent<GameControler>();
	}


	// Update is called once per frame
	void Update()
	{
		Vector3 NewCameraPos = new Vector3( transform.position.x, transform.position.y, CameraTransform.position.z );
		CameraTransform.position = NewCameraPos;


	}


	void OnCollisionEnter2D( Collision2D collision )
	{
	}


	void OnTriggerEnter2D( Collider2D col )
	{
		if( col.gameObject.tag == "Prize" )
		{
			mGameControler.AddScore();
			Destroy( col.gameObject );
		}
	}
}
