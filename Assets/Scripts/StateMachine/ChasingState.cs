
using System;
using StatePattern.Enemy;
using UnityEngine;

public class ChasingState : IState
{
    public EnemyController owner { get; set; }
    private IStateMachine stateMachine;
    private float timer;
    public ChasingState(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        Vector3 playerPosition = owner.GetTarget().Position;

        owner.Agent.isStopped = false;
        owner.Agent.SetDestination(playerPosition);
    }


    public void OnStateExit()
    {
        
    }

    public void Update()
    {
        
    }
}