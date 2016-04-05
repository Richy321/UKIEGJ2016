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
        public bool capturing;
		public Laz lazScript;
        public Color playerColour;

        private float hitBounceAmount = 3.5f;
        private Vector3 bounceTarget;
        private float bounceTime;
        private float bounceTimeMax = 0.5f;

        private float captureEmitInterval = 0.5f;
        private float captureEmitTime = 0.0f;
        private ParticleSystem.EmitParams captureEmitParams;


        public MeshRenderer CaptureRenderer;

        public GameObject RootGameObject;

        public ParticleSystem CaptureParticleSystem;

        public CharacterController CharController;

        public int PlayerNumber = 1;

        public enum PlayerState
        {
            None,
            TakingDamage
        }

        public PlayerState playerState;

        void Start()
        {
			firing = false;
            health = startHealth;
            playerState = PlayerState.None;

            captureEmitParams.startLifetime = 0.5f;
        }

        void Update()
        {
            UpdateMovement();

            UpdateAiming();

			UpdateFire ();

            UpdateCapture();
        }

        void UpdateMovement()
        {
            if (playerState == PlayerState.TakingDamage)
            {
                Debug.Log("BOUNCE");
                bounceTime += Time.deltaTime;

                transform.position = Vector3.Lerp(transform.position,
                    bounceTarget, Mathf.Clamp01(bounceTime / bounceTimeMax));

                if(bounceTime >= bounceTimeMax)
                    playerState = PlayerState.None;  
            }
            else
            {
                string horizontalString = "HorizontalMoveP" + PlayerNumber;
                string verticalString = "VerticalMoveP" + PlayerNumber;

                float horizontalMove = Input.GetAxis(horizontalString);
                float verticalMove = Input.GetAxis(verticalString);

                Vector3 delta = new Vector3(horizontalMove, 0.0f, verticalMove)*speed;
                Vector3 newPos = transform.position += delta*Time.deltaTime;

                CharController.SimpleMove(delta*Time.deltaTime);

                //transform.position = newPos;
            }
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

        void UpdateCapture()
        {
            string fire2 = "TrapP" + PlayerNumber;

            if (Input.GetAxis(fire2) > 0.1f)
            {
                if (!capturing)
                    CaptureParticleSystem.Emit(captureEmitParams, 100);

                capturing = true;
                CaptureRenderer.material.color = Color.yellow;

                if (captureEmitTime > captureEmitInterval)
                {
                    CaptureParticleSystem.Emit(captureEmitParams, 100);
                    captureEmitTime = 0.0f;
                }

                captureEmitTime += Time.deltaTime;
            }
            else
            {
                capturing = false;
                captureEmitTime = 0.0f;
                CaptureRenderer.material.color = playerColour;
            }
        }

        void UpdateFire(){

			string fire1 = "FireP" + PlayerNumber;
			
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

        public void TakeDamage(Enemy enemy)
        {
            if (playerState == PlayerState.TakingDamage)
                return;

            Debug.Log("P" + PlayerNumber + " took damage " + enemy.damage);
            health -= enemy.damage;

           /* if (health <= 0)
            {
                Die();
                return;
            }*/

            Vector3 bounceDir = (transform.position - enemy.gameObject.transform.position);
            Debug.Log("Bounce Dir: " + bounceDir);
            bounceDir.Normalize();
            
            bounceTime = 0.0f;

            bounceTarget = transform.position + bounceDir*hitBounceAmount;
            bounceTarget.y = transform.position.y; //keep height the same


            Debug.Log("bounceTarget: " + bounceTarget);

            playerState = PlayerState.TakingDamage;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
