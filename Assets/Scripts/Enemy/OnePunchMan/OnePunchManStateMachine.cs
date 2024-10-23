using StatePattern.StateMachine;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController>
    {
        private OnePunchManController Owner;
        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(StateMachine.States.IDLE, new IdleState<OnePunchManController>(this));
            states.Add(StateMachine.States.ROTATING, new RotatingState<OnePunchManController>(this));
            states.Add(StateMachine.States.SHOOTING, new ShootingState<OnePunchManController>(this));
        }
    }
}