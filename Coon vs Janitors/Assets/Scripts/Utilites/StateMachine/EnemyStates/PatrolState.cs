using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class PatrolState : IState
    {
        private readonly EnemyControll _enemyControll;
        private readonly NavMeshAgent _agent;
        private readonly Transform[] _patrolPoints;
        private readonly Animator _animator;
        private readonly float _waitTime;

        private float _waitTimer;
        private int _currentPointIndex;

        public PatrolState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, Transform[] patrolPoints, float waitTime)
        {
            _enemyControll = enemyControll;
            _animator = animator;
            _agent = agent;
            _patrolPoints = patrolPoints;
            _waitTime = waitTime;
        }

        public void OnEnter()
        {
            Debug.Log("Start Patrolling");
            if (_patrolPoints.Length > 0)
            {
                MoveToNextPoint();
            }
        }

        public void Update()
        {
            Debug.Log("Patrolling");
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _waitTimer += Time.deltaTime;

                if (_waitTimer >= _waitTime)
                {
                    MoveToNextPoint();
                    _waitTimer = 0f;
                }
            }
        }

        public void OnExit()
        {
            Debug.Log("Patrol Exit");
        }

        private void MoveToNextPoint()
        {
            if (_patrolPoints.Length == 0) return;

            _agent.destination = _patrolPoints[_currentPointIndex].position;
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
    }
}