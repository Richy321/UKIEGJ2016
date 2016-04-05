using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public List<Vector3> Waypoints;
    public int waypointIndex;

    public Transform target;

        void Start()
    {
        radiusCollider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Patrol:
                //move towards waypoint
                //change to next in distance
                break;
            case EnemyState.Chase:
                //move towards target if not null
                break;
            case EnemyState.Frozen:
                //no not fucking move you cunt
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

    public void Chase(Transform newTarget)
    {
        meshRend.material.color = Color.red;
        target = newTarget;
    }

    public void Capture()
    {
        meshRend.material.color = Color.magenta;
        mySection.EnemiesLeft--;
        Die();
    }

    public void Die()
    {
        Destroy(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && enemyState != EnemyState.Frozen)
        {
            Freeze();
        }

        if (enemyState == EnemyState.Frozen)
        {
            timeFrozen += Time.deltaTime;

            if (timeFrozen >= maxFrozen)
            {
                isCaptureable = true;
            }
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
