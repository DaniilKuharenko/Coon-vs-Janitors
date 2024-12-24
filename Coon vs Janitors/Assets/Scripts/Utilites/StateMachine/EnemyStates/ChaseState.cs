using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class ChaseState : IState
    {
        private readonly EnemyControll _enemyControl;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private StateMachine _stateMachine;

        public ChaseState(EnemyControll enemyControl, Animator animator, NavMeshAgent agent, StateMachine stateMachine)
        {
            _enemyControl= enemyControl;
            _animator = animator;
            _agent = agent;
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            Debug.Log("Chase Start");
        }

        public void Update()
        {
            Debug.Log("Chasing");
            if(_enemyControl.Target != null)
            {
                _agent.destination = _enemyControl.Target.position;

                // Check if the target selection can be performed
                CheckPickup();
            }
        }

        public void OnExit()
        {
            Debug.Log("Chase Exit");
        }
        
        // Check the pickup radius
        private void CheckPickup()
        {
            if (_enemyControl.Target == null) return;
            if (Vector3.Distance(_enemyControl.transform.position, _enemyControl.Target.position) < 2f) // attack radius
            {
                _stateMachine.SetState(_enemyControl.GetPickup());
            }
        }
    }
}
