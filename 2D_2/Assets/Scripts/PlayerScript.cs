using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float playerVelosity;
    public float boundary;
    private Vector3 playerPosition;

    private int playerLives;
    private int playerPoints;

    public AudioClip pointsSound;
    public AudioClip livesSound;

    public GUIStyle styleGUI;
    


    // Use this for initialization
    void Start()
    {
        playerLives = 3;
        playerPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition.x += Input.GetAxis("Horizontal") * playerVelosity;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerLives <=0)
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

    void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + " Score: " + playerPoints, styleGUI);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }


    public void TakeLives()
    {
        playerLives--;
        GetComponent<AudioSource>().PlayOneShot(livesSound);
    }


    public void AddPoints(int points)
    {
        playerPoints += points;
        GetComponent<AudioSource>().PlayOneShot(pointsSound);
    }
}
