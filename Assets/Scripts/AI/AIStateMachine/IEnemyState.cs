using UnityEngine;
using System.Collections;

/// <summary>
/// I enemy state. this is an interface which we can use to set up different behaviours for the AI to carry out.
/// </summary>

public interface IEnemyState {


	void UpdateState ();

	void OnTriggerEnter (Collider other);

	void ToPatrolState ();

	void ToAlertState ();

	void ToChaseState ();

}
