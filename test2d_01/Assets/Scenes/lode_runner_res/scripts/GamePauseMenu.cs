using UnityEngine;
using System.Collections;

public class GamePauseMenu : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void FixedUpdate()
	{
		if( Input.GetKeyUp( KeyCode.Escape ) )
		{
		
			Debug.Log( "KeyUp = Escape" );

			Application.LoadLevel( "menu" );
		}
	}
}
