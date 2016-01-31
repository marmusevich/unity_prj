using UnityEngine;
using System.Collections;

namespace Menu
{

    public class CircledMove : MonoBehaviour
    {
        public float SpeedAngle;
        public float StartAngle;
        private float mAngle;

        public float SizeX;
        public float SizeY;
        public Vector2 StartPos;

        /*
	public float SpeedAngleAxe;
	public float StartAngleAxe;
	private float mAngleAxe;
	public float mSizeX;
	public float mSizeY;
	*/

        Vector2 CalculatePos()
        {
            /*
		mSizeX = SizeX * Mathf.Sin (mAngleAxe + StartAngleAxe);
		mSizeY = SizeY * Mathf.Cos (mAngleAxe + StartAngleAxe);
		mAngleAxe += SpeedAngleAxe;
		*/

            float x = StartPos.x + SizeX * Mathf.Cos(mAngle + StartAngle);
            float y = StartPos.y + SizeY * Mathf.Sin(mAngle + StartAngle);

            mAngle += SpeedAngle;

            if(mAngle > 2 * Mathf.PI)
                mAngle = 0;
            if(mAngle < 0)
                mAngle = 2 * Mathf.PI;

            return new Vector2(x, y);
        }


        // Use this for initialization
        void Start()
        {
	
        }
	
        // Update is called once per frame
        void Update()
        {
            /*
		Vector2 pos = CalculatePos ();
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
		*/
        }

        void FixedUpdate()
        {
            Vector2 pos = CalculatePos();
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}
