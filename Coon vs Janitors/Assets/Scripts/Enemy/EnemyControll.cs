using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class EnemyControll : Enemy
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _waitTime = 2f;
        [SerializeField] private NavMeshAgent _agent;

        public Transform Target => _target;
        private Transform _target;

        private StateMachine _stateMachine;
        private EnemyIdleState _idleState;
        private EnemyWalkState _walkState;
        private PatrolState _patrolState;
        private ChaseState _chaseState;

        private float _checkSpeed;
        private int _currentPointIndex;

        public void InitializeEnemyControll()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState = new EnemyIdleState(this, _animator);
            _walkState = new EnemyWalkState(this, _animator);
            _patrolState = new PatrolState(this, _animator, _agent, _patrolPoints, _waitTime);
            _chaseState = new ChaseState(this, _animator, _agent);
            
            _stateMachine.AddTransition(_idleState, _walkState, new Predicate(() => _checkSpeed >= 0.5));
            _stateMachine.AddTransition(_walkState, _idleState, new Predicate(() => _checkSpeed <= 0.5));
            _stateMachine.AddTransition(_patrolState, _chaseState, new Predicate(() => _target != null));
            _stateMachine.AddTransition(_chaseState, _patrolState, new Predicate(() => _target == null));

            _stateMachine.SetState(_patrolState);
        }

        private void Update()
        {
            DetectTargets();
            CheckingSpeed();
            _stateMachine?.Update();
        }

        private void CheckingSpeed()
        {
            if (_target != null)
            {
                _checkSpeed = Vector3.Distance(transform.position, _target.position);
            }
            else
            {
                _checkSpeed = Mathf.MoveTowards(_checkSpeed, 0, Time.deltaTime * _moveSpeed);
            }
        }

        // Target detection in the horizontal plane (XZ)
        private void DetectTargets()
        {
            Collider[] detectedObjects = Physics.OverlapSphere(transform.position, VisionRange, VisionObstructingLayer);
            foreach (var detected in detectedObjects)
            {
                Vector3 directionToTarget = detected.transform.position - transform.position;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                if (angleToTarget <= VisionAngle / 2)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, directionToTarget, out hit, VisionRange, VisionObstructingLayer))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            _target = hit.transform;
                            return;
                        }
                    }
                }
            }
            _target = null;
        }
    }
}
