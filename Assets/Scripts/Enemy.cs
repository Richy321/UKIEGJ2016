using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class Enemy : MonoBehaviour
{
    public LevelSection mySection;

    public enum EnemyState
    {
        Patrol,
        Chase,
        Frozen
    }
    public EnemyState enemyState;
    private float timeFrozen = 0.0f;
    private float maxFrozen = 3.0f;
    private bool isCaptureable = false;

    private SphereCollider radiusCollider;
    public MeshRenderer meshRend;
	public Transform[] wayPoints;
	public int wayPointIndex = 0;
	public float switchY = 0.2f;
	public float patrolSpeed = 10.0f;
    public float damage = 25.0f;

    public float chaseSpeed = 2.0f;
    public Transform chaseTarget;
    public float chaseDistance = 5.0f;

    public Character[] characters;
    public bool AllowChasing = true;

    void Start()
    {
        radiusCollider = GetComponent<SphereCollider>();

        characters = FindObjectsOfType<Character>();
        Patrol ();
    }
    void Update()
    {
        CheckChaseDistances();

        switch (enemyState)
        {
		    case EnemyState.Patrol:
			    UpdatePatrol();
                break;
            case EnemyState.Chase:
                UpdateChase();
                break;
            case EnemyState.Frozen:
                UpdateFreeze();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Freeze()
    {
        enemyState = EnemyState.Frozen;
        timeFrozen = 0.0f;
        meshRend.material.color = Color.blue;
        Debug.Log("Freeze");
    }

    public void UpdateFreeze()
    {
        timeFrozen += Time.deltaTime;
        if (timeFrozen >= maxFrozen && !isCaptureable)
        {
            isCaptureable = true;
            meshRend.material.color = Color.cyan;
        }
    }

    public void UnFreeze()
    {
        timeFrozen = 0.0f;
        isCaptureable = false;

        if (chaseTarget)
            Chase();
        else
            Patrol();
        Debug.Log("UnFreeze");
    }

    public void Patrol()
    {
		meshRend.material.color = Color.green;
		enemyState = EnemyState.Patrol;
	}

	public void UpdatePatrol()
	{
		float step = patrolSpeed * Time.deltaTime;
		Vector3 target = wayPoints [wayPointIndex].transform.position;
		this.transform.position = Vector3.MoveTowards (transform.position, target, step);
		if (Mathf.Abs(Vector3.Distance (this.transform.position, wayPoints [wayPointIndex].transform.position)) <= switchY)
		{
			wayPointIndex++;
			if (wayPointIndex >= wayPoints.Length) {
				wayPointIndex = 0;
			}
		}
	}

    public void CheckChaseDistances()
    {
        if (enemyState == EnemyState.Frozen)
            return;

        if (chaseTarget != null && Mathf.Abs((transform.position - chaseTarget.position).magnitude) > chaseDistance)
        {
            chaseTarget = null;
        }

        if (!chaseTarget)
        {
            foreach (Character character in characters)
            {
                float dist = Mathf.Abs((character.gameObject.transform.position - transform.position).magnitude);
                if (dist <= chaseDistance)
                {
                    chaseTarget = character.gameObject.transform;
                    Chase();
                }
            }
        }

        if (!AllowChasing)
            chaseTarget = null;

        if (!chaseTarget)
            Patrol();
    }
    public void Chase()
    {
        meshRend.material.color = Color.red;
        enemyState = EnemyState.Chase;
    }

    public void UpdateChase()
    {
        if (chaseTarget)
        {
            float chaseStep = chaseSpeed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, chaseTarget.position, chaseStep);
        }
    }

    public void Capture()
    {
        meshRend.material.color = Color.magenta;
        if(mySection != null)
            mySection.EnemiesLeft--;
        Die();
    }

    public void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Capture" && isCaptureable)
        {
            Character characterScript = other.gameObject.GetComponentInParent<Character>();
            if (characterScript)
            {
                if (characterScript.capturing)
                    Capture();
            }
        }

        /*if (other.tag == "Player" && enemyState != EnemyState.Frozen)
        {
            if (!chaseTarget)
            {
                chaseTarget = other.gameObject.transform;
                Chase();
            }
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && enemyState != EnemyState.Frozen)
        {
            Freeze();
        }

        if (other.tag == "Player")
        {
            Character characterScript = other.gameObject.GetComponentInParent<Character>();
            characterScript.TakeDamage(this);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Character characterScript = collision.gameObject.GetComponentInParent<Character>();
            characterScript.TakeDamage(this);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bullet" && enemyState == EnemyState.Frozen)
        {
            if (other != radiusCollider)
                UnFreeze();
        }

        /*if (other.tag == "Player" && enemyState != EnemyState.Frozen)
        {
            chaseTarget = null;
            Patrol();
        }*/
    }
}
