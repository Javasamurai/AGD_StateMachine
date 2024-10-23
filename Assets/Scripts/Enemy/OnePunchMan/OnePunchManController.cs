using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private float idleTimer;
        private float shootTimer;
        private float targetRotation;
        private PlayerController target;

        private OnePunchManStateMachine onePunchManStateMachine;


        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            InitializeVariables();
            CreateStateMachine();
            onePunchManStateMachine.ChangeState(OnePunchManState.IDLE);
        }

        public PlayerController Target => target;

        private void CreateStateMachine()
        {
            onePunchManStateMachine = new OnePunchManStateMachine(this);
        }

        private void InitializeVariables()
        {
            idleTimer = enemyScriptableObject.IdleTime;
            shootTimer = enemyScriptableObject.RateOfFire;
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            // if(isIdle && !isRotating && !isShooting)
            // {
            //     idleTimer -= Time.deltaTime;
            //     if(idleTimer <= 0)
            //     {
            //         isIdle = false;
            //         isRotating = true;
            //         targetRotation = (Rotation.eulerAngles.y + 180) % 360;
            //     }
            // }

            // if(!isIdle && isRotating && !isShooting)
            // {
            //     SetRotation(CalculateRotation());
            //     if(IsRotationComplete())
            //     {
            //         isIdle = true;
            //         isRotating = false;
            //         ResetTimer();
            //     }
            // }

            // if(!isIdle && !isRotating && isShooting)
            // {
            //     Quaternion desiredRotation = CalculateRotationTowardsPlayer();
            //     SetRotation(RotateTowards(desiredRotation));
                
            //     if(IsFacingPlayer(desiredRotation))
            //     {
            //         shootTimer -= Time.deltaTime;
            //         if (shootTimer <= 0)
            //         {
            //             shootTimer = enemyScriptableObject.RateOfFire;
            //             Shoot();
            //         }
            //     }

            // }
            onePunchManStateMachine.Update();

        }

        private void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Rotation, desiredRotation) < Data.RotationThreshold;

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        
        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Rotation, desiredRotation, enemyScriptableObject.RotationSpeed / 30 * Time.deltaTime);

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            // isIdle = false;
            // isRotating = false;
            // isShooting = true;
            target = targetToSet;
            shootTimer = 0;
            onePunchManStateMachine.ChangeState(OnePunchManState.SHOOTING);
        }

        public override void PlayerExitedRange() 
        {
            // isIdle = true;
            // isRotating = false;
            // isShooting = false;
            onePunchManStateMachine.ChangeState(OnePunchManState.IDLE);
        }
    }
}