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

        private Vector3? _temporaryPoint; // Stores the current time point (if any)
        private float _temporaryPointPriority = 0.4f;
        private float _tempPointLifetime = 120.0f; // the Time point lifetime
        private float _tempPointCreatedTime; // The creation time of the time point is needed to keep track of how much time has passed since it was added

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

            RemoveExpiredTemporaryPoint();

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
            Vector3 destination;

            // the enemy is more likely to go to a time point than a normal one.
            if (_temporaryPoint.HasValue && Random.value < _temporaryPointPriority)
            {
                destination = _temporaryPoint.Value;
            }
            else
            {
                if (_patrolPoints.Length == 0) return;
                destination = _patrolPoints[_currentPointIndex].position;
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
            _agent.enabled = true;
            _agent.SetDestination(destination);
        }

        public void AddTemporaryPoint(Vector3 point)
        {
            _temporaryPoint = point;
            _tempPointCreatedTime = Time.time;
            Debug.Log($"Temporary point set to {point}");
        }

        private void RemoveExpiredTemporaryPoint()
        {
            if (_temporaryPoint.HasValue && Time.time - _tempPointCreatedTime >= _tempPointLifetime)
            {
                Debug.Log($"Temporary point expired at {_temporaryPoint.Value}");
                _temporaryPoint = null;
            }
        }
    }
}
