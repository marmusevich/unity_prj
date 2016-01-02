using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/*
 * распределять элементы на панэли
 * реагировать на изменение размера экрана
 */ 






public class MainMenuBehaviour : MonoBehaviour 
{
	public Button btnPattern;
	#region стандартные калбеки unity
	// Use this for initialization
	void Start () 
	{
		CreateMenuButton ();


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey("escape"))
			Application.Quit();	
	}
	#endregion


	#region обработчики событий кнопок
	public void OnButtonClick_btnExit()
	{
		Debug.Log ("OnButtonClick_btnExit");
		Application.Quit();
	}

	public void OnButtonClick_btnNext()
	{
		Debug.Log ("OnButtonClick_btnNext");
	}

	public void OnButtonClick_btnPrevious()
	{
		Debug.Log ("OnButtonClick_btnPrevious");
	}

	public void OnButtonClick_btnSceneSelect(MenuInfo.MenuOneItem sceneParam)
	{
		if (sceneParam == null)
			return;

		if (sceneParam.SceneName != null)
			Application.LoadLevel (sceneParam.SceneName);
		else
			Debug.Log ("Test (not presend scene name): " + sceneParam.Caption);
	}
	#endregion


	#region my
	public void CreateMenuButton ()
	{
		GameObject pnlScenes = GameObject.Find("pnlScenes");
		if (pnlScenes == null || btnPattern == null)
			return;

		MenuInfo.FillMenuItems ();

		int btnCount = 0;
		foreach (MenuInfo.MenuOneItem moi in MenuInfo.MenuItems)
		{
			Button btn = Instantiate(btnPattern);
			btn.name = string.Format("btnSceneSelect_{0}", btnCount);

			RectTransform rectTransform = btn.GetComponent<RectTransform>();
			rectTransform.SetParent(pnlScenes.transform);
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
			rectTransform.anchorMin = new Vector2(0, 1);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.localPosition = new Vector3(0.0f, pnlScenes.transform.position.y -30.0f * btnCount - 15, rectTransform.position.z );
			rectTransform.sizeDelta = new Vector2(1.0f, rectTransform.sizeDelta.y);

			// прослушыванее событий с именем сцены
			MenuInfo.MenuOneItem tmp = moi;
			btn.onClick.AddListener(() => OnButtonClick_btnSceneSelect(tmp));

			GameObject btnText = btn.transform.FindChild("Text").gameObject;
			if(btnText != null)
				btnText.GetComponent<Text>().text = moi.Caption;

			btnCount++;
		}
	}
	#endregion
}
