using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class ChaseState : IState
    {
        private readonly EnemyControll _enemyControl;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;

        public ChaseState(EnemyControll enemyControl, Animator animator, NavMeshAgent agent)
        {
            _enemyControl= enemyControl;
            _animator = animator;
            _agent = agent;
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

            float horizontalDistance = Vector2.Distance(
                new Vector2(_enemyControl.transform.position.x, _enemyControl.transform.position.z),
                new Vector2(_enemyControl.Target.position.x, _enemyControl.Target.position.z)
            );

            float heightDifference = Mathf.Abs(_enemyControl.Target.position.y - (_enemyControl.transform.position.y + _enemyControl.CircleHeight));

            if (horizontalDistance <= _enemyControl.PickupRadius && heightDifference <= 1f) // Adjusting the hit height
            {
                Debug.Log("Hit");
            }
        }
    }
}
