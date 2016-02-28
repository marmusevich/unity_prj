using System;
using UnityEngine;

namespace SpaceAttac
{

    public class MyCamera2DFollow : MonoBehaviour
    {
        public Transform target;
        // Use this for initialization
        private void Start()
        {
            //transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}