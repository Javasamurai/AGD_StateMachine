using System;
using StatePattern.Enemy;
using UnityEngine;

public class PatrollingState : IState
{
    private int patrolingIndex = -1;

    private Vector3 destination;

    public EnemyController owner { get; set; }
    private OnePunchManStateMachine stateMachine;

    public PatrollingState(OnePunchManStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        SetNewWayPoints();
        destination = GetDestination();
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        owner.Agent.isStopped = false;
        owner.Agent.SetDestination(destination);
    }

    private Vector3 GetDestination()
    {
        return owner.Data.PatrollingPoints[patrolingIndex];
    }

    private void SetNewWayPoints()
    {
        if (patrolingIndex == owner.Data.PatrollingPoints.Count -1)
        {
            patrolingIndex = 0;
        }
        else
        {
            patrolingIndex++;
        }
    }

    public void OnStateExit()
    {

    }

    public void Update()
    {
        if (ReachedDestination())
        {
            stateMachine.ChangeState(States.IDLE);
        }
    }

    private bool ReachedDestination()
    {
        return owner.Agent.remainingDistance <= owner.Agent.stoppingDistance;
    }
}