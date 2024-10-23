using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {

        private OnePunchManStateMachine onePunchManStateMachine;


        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            onePunchManStateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine()
        {
            onePunchManStateMachine = new OnePunchManStateMachine(this);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;
            onePunchManStateMachine.Update();

        }
    
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            onePunchManStateMachine.ChangeState(States.SHOOTING);
        }

        public override void PlayerExitedRange() 
        {
            onePunchManStateMachine.ChangeState(States.IDLE);
        }
    }
}