using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour
{
    public int hitsToKill;
    public int points;
    private int numberOfHits;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        numberOfHits = 0;
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ball")
        {
            numberOfHits++;

            if (numberOfHits >= hitsToKill)
            {
                if (player != null)
                {
                    player.SendMessage("AddPoints", points);
                }
                Destroy(this.gameObject);
            }
        }
    }
}

//создание из префаба
//	public GameObject asteroid;


//		for (int y = 0; y < 5; y++) {
//			for (int x = 0; x < 5; x++) {
//				Instantiate (asteroid, new Vector3 (x, y / 2 + 5, 0), Quaternion.identity);
//			}
//		}