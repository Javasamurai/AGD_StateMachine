using System;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : IStateMachine
    {
        private PatrolManController owner;
        private IState currentState;
        private Dictionary<States, IState> states = new Dictionary<States, IState>();
        public PatrolManStateMachine(PatrolManController owner)
        {
            this.owner = owner;
            CreateStates();
            SetOwner();
        }

        private void SetOwner()
        {
            foreach (var state in states.Values)
            {
                state.owner = owner;
            }
        }

        private void CreateStates()
        {
            states.Add(States.IDLE, new IdleState(this));
            states.Add(States.PATROLING, new PatrollingState(this));
            states.Add(States.CHASING, new ChasingState(this));
            states.Add(States.SHOOTING, new ShootingState(this));
            states.Add(States.ROTATING, new RotatingState(this));
        }

        public void ChangeState(States newState)
        {
            currentState?.OnStateExit();
            currentState = states[newState];
            currentState.OnStateEnter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}
