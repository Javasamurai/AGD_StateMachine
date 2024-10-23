using StatePattern.StateMachine;
using System;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : GenericStateMachine<PatrolManController>
    {
        private PatrolManController Owner;

        public PatrolManStateMachine(PatrolManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(StateMachine.States.IDLE, new IdleState<PatrolManController>(this));
            states.Add(StateMachine.States.PATROLLING, new PatrollingState<PatrolManController>(this));
            states.Add(StateMachine.States.CHASING, new ChasingState<PatrolManController>(this));
            states.Add(StateMachine.States.SHOOTING, new ShootingState<PatrolManController>(this));
            states.Add(StateMachine.States.ROTATING, new RotatingState<PatrolManController>(this));
        }
    }
}