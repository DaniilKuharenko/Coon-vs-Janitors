using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class PatrolState : IState
    {
        private readonly EnemyControll _enemyControll;
        protected readonly NavMeshAgent _agent;
        private readonly Transform[] _patrolPoints;
        protected readonly Animator _animator;
        private readonly float _waitTime;
        protected static readonly int WalkHash = Animator.StringToHash("Walking");
        protected static readonly int LoockHash = Animator.StringToHash("LookingDown");
        protected const float crossFadeDuration = 0.1f;

        private float _waitTimer;
        private bool _isWaiting;
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
            _animator.CrossFade(WalkHash, crossFadeDuration);
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
                // If the enemy is not waiting, start the inspection animation
                if (!_isWaiting)
                {
                    _animator.CrossFade(LoockHash, crossFadeDuration);
                    _isWaiting = true;
                }

                _waitTimer += Time.deltaTime;

                // If the waiting time has passed, move on
                if (_waitTimer >= _waitTime)
                {
                    MoveToNextPoint();
                    _waitTimer = 0f;
                    _isWaiting = false;
                    _animator.CrossFade(WalkHash, crossFadeDuration);
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