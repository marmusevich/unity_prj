using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public float playerVelosity;
    public float boundary;
    private Vector3 playerPosition;

	// Use this for initialization
	void Start () 
    {
        playerPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        playerPosition.x += Input.GetAxis("Horizontal") * playerVelosity;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerPosition.x < -boundary)
        {
            playerPosition.x = -boundary;
        }
        if (playerPosition.x > boundary)
        {
            playerPosition.x = boundary;
        }
        
        gameObject.transform.position = playerPosition;
	}
}
