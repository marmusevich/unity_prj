using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour
{
    //public GUISkin skinGUI;
    //public GUIStyle styleGUI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        //GUI.skin = skinGUI;
        //GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + " Score: " + playerPoints, styleGUI);

        //if (GUI.Button(new Rect(500.0f, 3.0f, 200.0f, 20.0f), "Pause"))
        //{
        //    Debug.Log("Pause press");
        //}
    }
}


//создание из префаба
//	public GameObject asteroid;


//		for (int y = 0; y < 5; y++) {
//			for (int x = 0; x < 5; x++) {
//				Instantiate (asteroid, new Vector3 (x, y / 2 + 5, 0), Quaternion.identity);
//			}
//		}




