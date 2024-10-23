using System;
using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloneState<T> : IState where T: EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloneState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateClone();
            CreateClone();
        }

        private void CreateClone()
        {
            CloneManController cloneManController = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
            cloneManController.Teleport();
            cloneManController.SetCloneCount((Owner as CloneManController).CloneCountLeft - 1);
            GameService.Instance.EnemyService.AddEnemy(cloneManController);
        }

        public void Update()
        {

        }

        public void OnStateExit()
        {

        }
    }
}