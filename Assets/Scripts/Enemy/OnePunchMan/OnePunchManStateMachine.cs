using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine
    {
        public OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<OnePunchManState, IState> states = new Dictionary<OnePunchManState, IState>();
        public OnePunchManStateMachine(OnePunchManController owner)
        {
            this.Owner = owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(OnePunchManState.IDLE, new IdleState(this));
            states.Add(OnePunchManState.ROTATING, new RotatingState(this));
            states.Add(OnePunchManState.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach (var state in states.Values)
            {
                state.owner = Owner;
            }
        }
        
        public void Update() => currentState?.Update();

        public void ChangeState(IState state)
        {
            currentState?.OnStateExit();
            currentState = state;
            currentState.OnStateEnter();
        }

        public void ChangeState(OnePunchManState state)
        {
            ChangeState(states[state]);
        }
    }
}
