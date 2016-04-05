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

    public Transform target;

        void Start()
    {
		float step = patrolSpeed * Time.deltaTime;
        radiusCollider = GetComponent<SphereCollider>();
		Patrol ();
    }
    void Update()
    {
        switch (enemyState)
        {
		case EnemyState.Patrol:
			UpdatePatrol ();
                break;
            case EnemyState.Chase:
                //move towards target if not null
                break;
            case EnemyState.Frozen:
                    timeFrozen += Time.deltaTime;
                    if (timeFrozen >= maxFrozen && !isCaptureable)
                    {
                        isCaptureable = true;
                        meshRend.material.color = Color.cyan;
                    }
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

    public void UnFreeze()
    {
        timeFrozen = 0.0f;
        isCaptureable = false;
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

    public void Chase(Transform newTarget)
    {
        meshRend.material.color = Color.red;
        target = newTarget;
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
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && enemyState != EnemyState.Frozen)
        {
            Freeze();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bullet" && enemyState == EnemyState.Frozen)
        {
            if(other != radiusCollider)
                UnFreeze();
        }
    }
}
