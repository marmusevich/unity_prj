using UnityEngine;
using System.Collections;

namespace SpaceAttac
{

    public class AsteroidController : MonoBehaviour
    {

        public int health = 10;


        // Use this for initialization
        void Start()
        {
	
        }
	
        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z);
        }
    }
}