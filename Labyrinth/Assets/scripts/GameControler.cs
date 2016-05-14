using UnityEngine;
using System.Collections;


using MazePoint = IntVector2D;
using MazePointListType = System.Collections.Generic.List<IntVector2D>;
using MazeType = System.UInt16;


public class GameControler : MonoBehaviour 
{
	#region переменные
	/// <summary>
	/// Размер лабиринта
	/// </summary>
	public uint xMazeSize = 20;
	public uint yMazeSize = 20;

	/// <summary>
	/// процент извилистости
	/// </summary>
	public float WindingPrecent = 0.5f;



	public int CountPrize = 10;
	public int PrizeCost = 10;
	public int CurrentCountPrize = 0;

	public GameObject PrefabPrize;


	private int playerLives = 3;
	private int playerPoints = 0;


	private MazeController mMazeController;
	//алгоритм поиска пути
	private FindPath mFindPath = null;

	private GameObject mHero;

	#endregion

	#region функции
	//перезапуск уровня
	public void ReStart()
	{
		ClearMaze();
		mMazeController.Generate();
		mMazeController.Draw();
		mFindPath.SetMaze( mMazeController.GetMazeArr() );
		PositPlaer();
		PositPrize();
	}

	//увеличить счет
	public void AddScore()
	{

		Debug.Log( "AddScore()"  );


		CurrentCountPrize++;

		playerPoints += PrizeCost;

		if( CurrentCountPrize >= CountPrize )
		{
			ReStart();
			CurrentCountPrize = 0;
		}
	}

	//очистить уровень
	void ClearMaze()
	{
		GameObject[] wall = GameObject.FindGameObjectsWithTag( "Wall" );
		foreach( GameObject w in wall )
			Destroy( w );

		GameObject[] prize = GameObject.FindGameObjectsWithTag( "Prize" );
		foreach( GameObject p in prize )
			Destroy( p );
	}

	// разместить героя
	void PositPlaer()
	{
		Vector2 vec = mMazeController.ConvertMazeCoordToScreen( mMazeController.ListSpace[ mMazeController.ListSpace.Count - 1 ].x, mMazeController.ListSpace[ mMazeController.ListSpace.Count - 1 ].y );
		mMazeController.ListSpace.RemoveAt( mMazeController.ListSpace.Count - 1 );
		mHero.transform.position = new Vector3( vec.x, vec.y, mHero.transform.position.z );
	}


	//разместить призы
	void PositPrize()
	{
		if( PrefabPrize != null )
		{
			for(int i = 0 ; i < CountPrize ; i++)
			{
				if( mMazeController.ListSpace.Count < 1 )
					break;
				int index = UnityEngine.Random.Range( 0, mMazeController.ListSpace.Count );
				Vector2 posPriz = mMazeController.ConvertMazeCoordToScreen( mMazeController.ListSpace[ index ].x, mMazeController.ListSpace[ index ].y );
				GameObject obj = Instantiate( PrefabPrize, posPriz, Quaternion.identity ) as GameObject;
				obj.name = string.Format( "Priz_{0}", i );
				mMazeController.ListSpace.RemoveAt( index );
			}
		}
	}


	void StartFindPath(Vector2 finishPosInWorldPoint)
	{
		MazePoint finishPos = mMazeController.ConvertScreenCoordToMaze( finishPosInWorldPoint );

		Vector2 startPosInWorldPoint = new Vector2( mHero.transform.position.x, mHero.transform.position.y);
		MazePoint startPos = mMazeController.ConvertScreenCoordToMaze( startPosInWorldPoint );


		Debug.Log( "startPos: " + startPos.ToString() +  ", finishPos: " + finishPos.ToString() );


		MazePointListType path = mFindPath.GetPath( startPos, finishPos );
		if(path!=null)
			StartCoroutine(MoveHeroOnPath(path));
	}

	// после поиска пути запус сопрограммы перемещения
	#endregion

	IEnumerator MoveHeroOnPath(MazePointListType path)
	{
		//Debug.Log( "path.Count: "+ path.Count.ToString() );

		MazePoint pos;
		for(int i =0; i<path.Count; i++)
		{
			pos = path[ i ];
			Debug.Log("[ "+ i.ToString() + " ] -> "+   pos.ToString() );

			Vector2 vec = mMazeController.ConvertMazeCoordToScreen( pos.x, pos.y );
			mHero.transform.position = new Vector3( vec.x, vec.y, mHero.transform.position.z );	

			yield return new WaitForSeconds( 0.2f );
			//yield return new WaitForFixedUpdate();
		}
	}








	#region стандартные калбэки юнити
	// Use this for initialization
	void Start () 
	{
		mHero = GameObject.FindGameObjectWithTag( "Player" );

		mFindPath = new FindPath();
		mMazeController = GetComponent<MazeController>();
		mMazeController.SetNewSize( xMazeSize, yMazeSize );
		mMazeController.SetNewGenerator( new GrowingTreeMezeGen( WindingPrecent ) );

		ReStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			ReStart();
		}

		//отработка клика мыши
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Vector3 v3 = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			StartFindPath( new Vector2( v3.x, v3.y ));
		}
	}


	void OnGUI()
	{
		GUI.Label( new Rect( 5.0f, 3.0f, 200.0f, 200.0f ), "Live's: " + playerLives + " Score: " + playerPoints );
	}

	#endregion
}
