using System;
using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T: EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public TeleportingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            TeleportToRandomLocation();
        }

        private void TeleportToRandomLocation()
        {
            Owner.Agent.Warp(GetRandomNavMeshPoints());
            stateMachine.ChangeState(States.CHASING);
        }

        private Vector3 GetRandomNavMeshPoints()
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 5 + Owner.Position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return Owner.Position;
        }

        public void Update()
        {
            
        }

        public void OnStateExit()
        {

        }
    }
}