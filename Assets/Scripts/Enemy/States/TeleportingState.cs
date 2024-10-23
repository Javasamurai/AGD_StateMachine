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
        private float timer;

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
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * Owner.Data.TeleportingRadius + Owner.Position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, Owner.Data.TeleportingRadius, NavMesh.AllAreas))
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