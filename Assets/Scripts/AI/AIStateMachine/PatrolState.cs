﻿using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private int nextWayPoint;

	public PatrolState (StatePatternEnemy statePatternEnemy){
		enemy = statePatternEnemy;
	}

	public void UpdateState () {

		//Look ();
		patrol ();
	}

    
	public void OnTriggerEnter (Collider other)
    {
		if (other.gameObject.CompareTag ("Player"))
			ToAlertState ();
        if (other.gameObject.CompareTag("Bullet"))
            ToFrozen();
	}

	public void ToPatrolState (){
		Debug.Log ("Cant Transition to same state");
	}

	public void ToAlertState (){
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState (){	
		enemy.currentState = enemy.chaseState;
	}

	private void Look(){
		RaycastHit hit;
		if (Physics.Raycast (enemy.eyeHeight.position, enemy.eyeHeight.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag ("Player")) {
			enemy.chaseTarget = hit.transform;
			ToChaseState ();
		}
	}

    public void ToFrozen()
    {

    }

    //public void ToCaptureable()
    //{

    //}


    void patrol() {

		enemy.meshRendererFlag.material.color = Color.green;
		enemy.navMeshAgent.destination = enemy.wayPoints [nextWayPoint].position;
		enemy.navMeshAgent.Resume ();

		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending) {
			nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
		}
	}
}
