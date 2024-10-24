using System;
using StatePattern.Enemy;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RotatingState : IState
{
    public EnemyController owner { get; set; }

    private float targetRotation;
    private IStateMachine statemachine;

    public RotatingState(IStateMachine stateMachine)
    {
        this.statemachine = stateMachine;
    }

    public void OnStateEnter()
    {
        targetRotation = (owner.Rotation.eulerAngles.y + 180) % 360;
    }

    public void OnStateExit()
    {
        targetRotation = 0;
    }

    public void Update()
    {
        owner.SetRotation(CalculateRotation());
        if (RotationComplete())
        {
            statemachine.ChangeState(States.IDLE);
        }
    }

    private Vector3 CalculateRotation()
    {
        return Vector3.up * Mathf.MoveTowardsAngle(owner.Rotation.eulerAngles.y, targetRotation, owner.Data.RotationSpeed * Time.deltaTime);
    }

    private bool RotationComplete()
    {
        return Mathf.Abs(Mathf.Abs(owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < owner.Data.RotationThreshold;
    }
}