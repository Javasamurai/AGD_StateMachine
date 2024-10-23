using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.StateMachine;

public class GenericStateMachine<T> where T : EnemyController
{
    protected T owner;
    protected IState state;
    protected Dictionary<States, IState> states = new Dictionary<States, IState>();

    public GenericStateMachine(T owner)
    {
        this.owner = owner;
    }

    public void ChangeState(IState state)
    {
        if (this.state!= null)
        {
            this.state.OnStateExit();
        }
        this.state = state;
        this.state.OnStateEnter();
    }

    public void SetOwner()
    {
        foreach (IState s in states.Values)
        {
            s.Owner = owner;
        }
    }

    public void ChangeState(States state) => ChangeState(states[state]);

    public void Update()
    {
        this.state?.Update();
    }
}