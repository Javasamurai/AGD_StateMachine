using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine
    {
        public EnemyController Owner;
        private IState currentState;
        protected Dictionary<States, IState> states = new Dictionary<States, IState>();
        public OnePunchManStateMachine(OnePunchManController owner)
        {
            this.Owner = owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(States.IDLE, new IdleState(this));
            states.Add(States.ROTATING, new RotatingState(this));
            states.Add(States.SHOOTING, new ShootingState(this));
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

        public void ChangeState(States state)
        {
            ChangeState(states[state]);
        }
    }
}
