using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace Raccons_House_Games
{
    public class PatrolState : IState
    {
        private readonly EnemyControll _enemyControll;
        protected readonly NavMeshAgent _agent;
        private readonly List<Transform> _patrolPoints;
        protected readonly Animator _animator;
        private readonly float _waitTime;
        protected static readonly int WalkHash = Animator.StringToHash("Walking");
        protected static readonly int LoockHash = Animator.StringToHash("LookingDown");
        protected const float crossFadeDuration = 0.1f;
        private readonly float _tempPointLifetime = 300.0f; // Time point lifetime

        private float _waitTimer;
        private bool _isWaiting;
        private int _currentPointIndex;
        private Transform _tempPoint;

        public PatrolState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, Transform[] patrolPoints, float waitTime)
        {
            _enemyControll = enemyControll;
            _animator = animator;
            _agent = agent;
            _patrolPoints = new List<Transform>(patrolPoints);
            _waitTime = waitTime;
        }

        public void OnEnter()
        {
            Debug.Log("Start Patrolling");
            _animator.CrossFade(WalkHash, crossFadeDuration);
            if (_patrolPoints.Count > 0)
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

            RemoveTempPointIfExpired();
        }

        public void OnExit()
        {
            Debug.Log("Patrol Exit");
        }

        private void MoveToNextPoint()
        {
            if (_patrolPoints.Count == 0) return;

            // Increase the probability of selecting a time point
            bool useTempPoint = _tempPoint != null && Random.value < 0.5f;

            if(useTempPoint)
            {
                _agent.destination = _tempPoint.position;
            }
            else
            {
                _agent.destination = _patrolPoints[_currentPointIndex].position;
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
            }
        }
        
        public void CreateTempPoint(Vector3 position)
        {
            if(_tempPoint != null)
            {
                Object.Destroy(_tempPoint.gameObject);
            }

            GameObject tempPointObject = new GameObject("TempPatrolPoint");
            tempPointObject.transform.position = position;
            
            // Deleting a point after a certain time
            Object.Destroy(tempPointObject, _tempPointLifetime);
        }

        private void RemoveTempPointIfExpired()
        {
            if(_tempPoint == null) return;

            if(_tempPointLifetime <= 0f)
            {
                _patrolPoints.Remove(_tempPoint);
                Object.Destroy(_tempPoint.gameObject);
                _tempPoint = null;
            }
        }
    }
}