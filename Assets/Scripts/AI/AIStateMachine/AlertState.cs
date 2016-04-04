using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private float searchTime;

	public AlertState (StatePatternEnemy statePatternEnemy){
		enemy = statePatternEnemy;
	}

	public void UpdateState () {
		Look ();
		Search ();

	}

	public void OnTriggerEnter (Collider other){

	}

	public void ToPatrolState (){
		enemy.currentState = enemy.patrolState;
		searchTime = 0f;
	}

	public void ToAlertState (){
		Debug.Log ("Cant Transition to same state");
	}

	public void ToChaseState (){	
		enemy.currentState = enemy.chaseState;
		searchTime = 0f;
	}

	private void Look(){
		RaycastHit hit;
		if (Physics.Raycast (enemy.eyeHeight.position, enemy.eyeHeight.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag ("Player")) {
			enemy.chaseTarget = hit.transform;
			ToChaseState ();
		}
	}

	private void Search(){

		enemy.meshRendererFlag.material.color = Color.yellow;
		enemy.navMeshAgent.Stop ();
		enemy.transform.Rotate (0, enemy.searchingSpeed * Time.deltaTime, 0);
		searchTime += Time.deltaTime;

		if (searchTime >= enemy.searchingDuration)
			ToPatrolState ();
	}
}

