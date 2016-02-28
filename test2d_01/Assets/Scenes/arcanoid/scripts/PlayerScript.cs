using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace Arcanoid
{

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

        public GameObject ballObject;


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

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if(playerLives <= 0)
            {
                Application.Quit();
            }

            if(playerPosition.x < -boundary)
            {
                playerPosition.x = -boundary;
            }
            if(playerPosition.x > boundary)
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


        public void Save()
        {
            PlayerPrefs.SetInt("playerLives", playerLives);
            PlayerPrefs.SetInt("playerPoints", playerPoints); 
        }

        public void Load()
        {
            playerLives = PlayerPrefs.GetInt("playerLives");
            playerPoints = PlayerPrefs.GetInt("playerPoints"); 
        }
    }

}