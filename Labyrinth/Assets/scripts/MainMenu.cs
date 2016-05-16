using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public void StartGameButtonPreset() 
	{
		SceneManager.LoadScene( "maze" );
	}

	public void SettingButtonPreset() 
	{
		Debug.Log( "SettingButtonPreset()");

	}


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
