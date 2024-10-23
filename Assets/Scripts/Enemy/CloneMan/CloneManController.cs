using StatePattern.Enemy;
using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine stateMachine;
        public int CloneCountLeft { get; set; }

        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
            SetCloneCount(enemyScriptableObject.CloneCount);
        }

        public void SetCloneCount(int count)
        {
            CloneCountLeft = count;
        }

        public override void Die()
        {
            if (CloneCountLeft > 0)
            {
                stateMachine.ChangeState(States.CLONING);
            }
            base.Die();
        }

        public void Teleport()
        {
            stateMachine.ChangeState(States.TELEPORTING);
        }

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.SHOOTING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);
    }
}
