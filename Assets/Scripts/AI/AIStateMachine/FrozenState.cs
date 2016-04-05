using UnityEngine;
using System.Collections;

public class FrozenState : IEnemyState
{

    private readonly StatePatternEnemy enemy;
    private float timeSpentFrozen = 0.0f;
    private float maxFrozen = 3.0f;

    public FrozenState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Debug.Log("im fcking frozen");
        if (!enemy.isFrozen) 
             Freeze();
        Debug.Log("im fcking frozen");
        if (enemy.isFrozen)
            timeSpentFrozen += Time.deltaTime;
        
    }

    public void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.CompareTag("Player"))
         //   ToAlertState();
    }

    public void ToPatrolState()
    {
        
    }

    public void ToAlertState()
    {
       // enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
       // enemy.currentState = enemy.chaseState;
    }


    public void ToFrozen()
    {
        Debug.Log("Cant change to current state");
    }

    //public void ToCaptureable()
    //{
    //    if (enemy.isFrozen && timeSpentFrozen > maxFrozen )
    //    {
    //        enemy.currentState = enemy.captureableState;
    //    }
    //}


    void Freeze()
    {
        timeSpentFrozen = 0.0f;
        
        enemy.meshRendererFlag.material.color = Color.blue;
        enemy.navMeshAgent.Stop();
        enemy.isFrozen = true;
        

        

    }
}