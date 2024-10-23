namespace StatePattern.Enemy
{
    public interface IState
    {
        public EnemyController owner { get; set; }
        public void OnStateEnter();
        public void OnStateExit();
        public void Update();
    }
}


public enum States
{
    IDLE,
    ROTATING,
    SHOOTING,
    PATROLING,
    CHASING
}