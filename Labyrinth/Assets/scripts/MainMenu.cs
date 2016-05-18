using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public Animator startButton;
	public Animator dlg_setting;



	public void StartGameButtonPreset()
	{
		SceneManager.LoadScene( "maze" );
	}



	public void OpenSettings()
	{
		startButton.SetBool( "isHidden", true );
		dlg_setting.SetBool( "isHidden", false );
		dlg_setting.enabled = true;
	}

	public void CloseSettings()
	{
		startButton.SetBool( "isHidden", false );
		dlg_setting.SetBool( "isHidden", true );
	}

	public void ToggleMenu()
	{
		//contentPanel.enabled = true;

		//bool isHidden = contentPanel.GetBool( "isHidden" );
//		contentPanel.SetBool("isHidden", !isHidden);
//		gearImage.enabled = true;
//		gearImage.SetBool("isHidden", !isHidden);
	}



	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
