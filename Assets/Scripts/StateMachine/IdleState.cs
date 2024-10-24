
using System;
using StatePattern.Enemy;
using UnityEngine;

public class IdleState : IState
{
    public EnemyController owner { get; set; }
    private IStateMachine stateMachine;
    private float timer;
    public IdleState(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        timer = owner.Data.IdleTime;
    }

    public void OnStateExit()
    {
        timer = 0;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            stateMachine.ChangeState(States.ROTATING);
        }
    }
}