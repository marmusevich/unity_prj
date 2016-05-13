using UnityEngine;
using System.Collections;

public class MazeHeroController : MonoBehaviour
{

	public Transform CameraTransform;

	public float MaxSpeed = 10.0f;
	private int playerLives = 3;
	private int playerPoints = 0;

	private Rigidbody2D rigbody;



	public int CountPrize = 10;
	public int CurrentCountPrize = 0;
	public Transform CodeStore;
	private MazeController mazeController;

	//алгоритм поиска пути
	private FindPath mFindPath = null;


	// Use this for initialization
	void Start()
	{
		rigbody = GetComponent<Rigidbody2D>();

		mFindPath = new FindPath();

		mazeController = CodeStore.GetComponent<MazeController>();
		CountPrize = mazeController.CountPrize;

	}

	private void FixedUpdate()
	{
		float moveX = Input.GetAxis( "Horizontal" );
		float moveY = Input.GetAxis( "Vertical" );
		rigbody.velocity = new Vector2( moveX * MaxSpeed, moveY * MaxSpeed );
	}


	// Update is called once per frame
	void Update()
	{
		Vector3 NewCameraPos = new Vector3( transform.position.x, transform.position.y, CameraTransform.position.z );
		CameraTransform.position = NewCameraPos;


		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			mazeController.ReStart();
		}



		if( Input.GetMouseButtonDown( 0 ) )
		{
			Vector3 v3 = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			Vector2 v2 = new Vector2( v3.x, v3.y );


			Debug.Log( "WorldPoint: " + v2.ToString() +
			", maze = " + mazeController.ConvertScreenCoordToMaze( v2 ).ToString() );

		}

	}

	void OnGUI()
	{
		GUI.Label( new Rect( 5.0f, 3.0f, 200.0f, 200.0f ), "Live's: " + playerLives + " Score: " + playerPoints );
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
	}


	void OnTriggerEnter2D( Collider2D col )
	{
		if( col.gameObject.tag == "Prize" )
		{
			Destroy( col.gameObject );

			playerPoints += 10;
			CurrentCountPrize++;

			if( CurrentCountPrize >= CountPrize )
			{
				mazeController.ReStart();
				CurrentCountPrize = 0;
			}
		}
	}
}
