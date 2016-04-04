using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Character : MonoBehaviour
    {
        public float health;
        public float startHealth = 100.0f;
        public float speed = 2.0f;
        public float angular = 5.0f;
		public bool firing;
		public bool trapping;
		public Laz lazScript;

        public GameObject RootGameObject;

        public int PlayerNumber = 1;

        void Start()
        {
			firing = false;
			trapping = false;
            health = startHealth;
        }

        void Update()
        {
            UpdateMovement();

            UpdateAiming();

			UpdateFire ();
        }

        void UpdateMovement()
        {
            string horizontalString = "HorizontalMoveP" + PlayerNumber;
            string verticalString = "VerticalMoveP" + PlayerNumber;

            float horizontalMove = Input.GetAxis(horizontalString);
            float verticalMove = Input.GetAxis(verticalString);

            Vector3 delta = new Vector3(horizontalMove, 0.0f, verticalMove) * speed;
            Vector3 newPos = transform.position += delta * Time.deltaTime;

            transform.position = newPos;

        }

        void UpdateAiming()
        {
            string horizontalString = "HorizontalAimP" + PlayerNumber;
            string verticalString = "VerticalAimP" + PlayerNumber;

            float horizontalAim = Input.GetAxis(horizontalString);
            float verticalAim = Input.GetAxis(verticalString);


            if (Mathf.Abs(horizontalAim) > 0 || Mathf.Abs(verticalAim) > 0 && (Mathf.Abs(horizontalAim) + Mathf.Abs(verticalAim) > 0.5f))
            {
                Vector3 lookDir = new Vector3(horizontalAim, 0.0f, verticalAim);
                RootGameObject.transform.rotation = Quaternion.LookRotation(lookDir);

                /*float rotateAmount = 0.0f;
                float angleOffset = 0.0f;

                if (horizontalAim > 0 && verticalAim > 0) { // ++ 
                    rotateAmount = Mathf.Atan(verticalAim / horizontalAim) * 180.0f / Mathf.PI - 90.0f;
                }
                else if (horizontalAim < 0 && verticalAim > 0)
                {
                    // -+
                    rotateAmount = 90.0f - Mathf.Atan(-verticalAim / horizontalAim) * 180.0f / Mathf.PI;
                }
                else if (horizontalAim < 0 && verticalAim < 0)
                {
                    //--
                    rotateAmount = 90.0f + Mathf.Atan(verticalAim / horizontalAim) * 180.0f / Mathf.PI;
                } else if (horizontalAim > 0 && verticalAim < 0)
                {
                    rotateAmount = 180.0f + (90.0f - Mathf.Atan(verticalAim / -horizontalAim) * 180.0f / Mathf.PI);
                }
                
                RootGameObject.transform.localEulerAngles = new Vector3(0.0f, -rotateAmount, 0.0f);*/

            }
        }

		void UpdateFire(){

			string fire1 = "FireP" + PlayerNumber;
			//string fire2 = "TrapP" + PlayerNumber;

			if (Input.GetAxis (fire1) > 0.1f) {
				if (!firing) {
					lazScript.startLaz ();
					firing = true;
					Debug.Log ("Fire Start");
				} 
			}

			if ((Input.GetAxis(fire1) < 0.1f) && firing) 
			{
						lazScript.stopLaz ();
						firing = false;
						Debug.Log ("Stop");
			}
		}
    }
}
