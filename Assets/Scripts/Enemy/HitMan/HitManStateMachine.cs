using StatePattern.StateMachine;
using System;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class HitManStateMachine : GenericStateMachine<HitManController>
    {
        private HitManController Owner;

        public HitManStateMachine(HitManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(StateMachine.States.IDLE, new IdleState<HitManController>(this));
            states.Add(StateMachine.States.PATROLLING, new PatrollingState<HitManController>(this));
            states.Add(StateMachine.States.CHASING, new ChasingState<HitManController>(this));
            states.Add(StateMachine.States.SHOOTING, new ShootingState<HitManController>(this));
            states.Add(StateMachine.States.TELEPORTING, new TeleportingState<HitManController>(this));
        }
    }
}