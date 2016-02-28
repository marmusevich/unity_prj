using UnityEngine;
using System.Collections;

namespace HabrTest1
{

    public class CameraFollow : MonoBehaviour
    {

        public Transform target;


        private void Start()
        {
        }


        private void Update()
        {
            Vector3 NewCameraPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = NewCameraPos;
        }
    }
}