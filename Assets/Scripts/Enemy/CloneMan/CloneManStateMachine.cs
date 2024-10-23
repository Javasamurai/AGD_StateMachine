using StatePattern.StateMachine;
using System;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class CloneManStateMachine : GenericStateMachine<CloneManController>
    {
        private CloneManController Owner;

        public CloneManStateMachine(CloneManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(StateMachine.States.IDLE, new IdleState<CloneManController>(this));
            states.Add(StateMachine.States.PATROLLING, new PatrollingState<CloneManController>(this));
            states.Add(StateMachine.States.CHASING, new ChasingState<CloneManController>(this));
            states.Add(StateMachine.States.SHOOTING, new ShootingState<CloneManController>(this));
            states.Add(StateMachine.States.TELEPORTING, new TeleportingState<CloneManController>(this));
            states.Add(StateMachine.States.CLONING, new CloneState<CloneManController>(this));
        }
    }
}