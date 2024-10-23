using System;
using StatePattern.Enemy;
using UnityEngine;

public class ShootingState : IState
{
    public EnemyController owner { get; set; }

    private OnePunchManStateMachine statemachine;

    private float shootTimer;

    private Quaternion desiredRotation;

    public ShootingState(OnePunchManStateMachine stateMachine)
    {
        this.statemachine = stateMachine;
    }

    public void OnStateEnter()
    {
        desiredRotation = CalculateRotationTowardsPlayer();
        owner.SetRotation(desiredRotation);
    }

    private Quaternion CalculateRotationTowardsPlayer()
    {
        Vector3 directionToPlayer = owner.GetTarget().Position - owner.Position;
        directionToPlayer.y = 0f;
        return Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }

    public void OnStateExit()
    {
        shootTimer = 0f;
    }

    public void Update()
    {
        if(IsFacingPlayer(desiredRotation))
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = owner.Data.RateOfFire;
                owner.Shoot();
            }
        }
    }
    private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(owner.Rotation, desiredRotation) < owner.Data.RotationThreshold;

}