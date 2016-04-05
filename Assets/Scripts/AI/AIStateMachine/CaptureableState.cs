//using UnityEngine;
//using System.Collections;

//public class CaptureableState : IEnemyState
//{

//    private readonly StatePatternEnemy enemy;


//    public CaptureableState(StatePatternEnemy statePatternEnemy)
//    {
//        enemy = statePatternEnemy;
//    }

//    public void UpdateState()
//    {

//    }

//    public void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//            ToAlertState();
//    }

//    public void ToPatrolState()
//    {
//      //  Debug.Log("Cant Transition to same state");
//    }

//    public void ToAlertState()
//    {
//     //   enemy.currentState = enemy.alertState;
//    }

//    public void ToChaseState()
//    {
//     //   enemy.currentState = enemy.chaseState;
//    }

//    public void ToFrozen()
//    {
     
//    }

//    public void ToCaptureable()
//    {
//        Debug.Log("Cant Transition to same state");
//    }


  
//}