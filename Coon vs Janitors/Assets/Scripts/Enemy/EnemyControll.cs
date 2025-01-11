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
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _trashPoint;
        [SerializeField] private Transform _shedPoint;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private TrashController _trashController;

        public Transform Target => _target;
        private Transform _target;

        public StateMachine _stateMachine;
        private EnemyIdleState _idleState;
        private EnemyWalkState _walkState;
        private PatrolState _patrolState;
        private ChaseState _chaseState;
        private EnemyPickupState _enemyPickupState;
        private EnemyPickupPlayerState _enemyPickupPlayerState;
        private RunToSoundState _runToSoundState;

        private float _checkSpeed;
        private float _targetLostTime = 3.0f; // Target “memorization” time
        private float _currentTargetLostTime;

        public void InitializeEnemyControll()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (_trashController == null)
            {
                _trashController = FindFirstObjectByType<TrashController>();
            }
        }

        public void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState = new EnemyIdleState(this, _animator);
            _walkState = new EnemyWalkState(this, _animator);
            _chaseState = new ChaseState(this, _animator, _agent, _stateMachine);
            _patrolState = new PatrolState(this, _animator, _agent, _patrolPoints, _waitTime);
            _enemyPickupState = new EnemyPickupState(this, _animator, _agent, _holdPoint, 
            _trashPoint, _stateMachine, _trashController);
            _enemyPickupPlayerState = new EnemyPickupPlayerState(this, _animator, _agent, _holdPoint, 
            _shedPoint, _stateMachine);
            
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
                        int hitLayer = hit.collider.gameObject.layer;
                        if(((1 << hitLayer) & _targetLayerMask) != 0)
                        {
                            if(hitLayer == LayerMask.NameToLayer("HiddenLayer"))
                            {
                                GetPatrolState().AddTemporaryPoint(transform.position);
                                _target = null;
                            }
                            else
                            {
                                _target = hit.transform;
                                _currentTargetLostTime = _targetLostTime;
                            }
                        }
                    }
                }
            }
            
            // If the target is not found, start the countdown
            if (_target != null)
            {
                _currentTargetLostTime -= Time.deltaTime;

                if (_currentTargetLostTime <= 0) // Time's up, lose the target
                {
                    // After losing the target, create a temporary point
                    GetPatrolState().AddTemporaryPoint(transform.position);
                    _target = null;
                }
            }
        }

        public void EnemyAlert()
        {
            // Find all objects in the detection radius
            Collider[] detectedObjects = Physics.OverlapSphere(transform.position, DetectionRadius);
            
            // Variable to store the nearest object
            Transform closestObject = null;
            float closestDistance = float.MaxValue;

            foreach (var obj in detectedObjects)
            {
                if (obj.CompareTag("SoundSource"))
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj.transform;
                    }
                }
            }

            if (closestObject != null && closestDistance <= DetectionRadius)
            {
                Debug.Log($"Fallen object detected within range. Distance: {closestDistance}");
                _runToSoundState = new RunToSoundState(this, _animator, _agent, _stateMachine, transform, closestDistance);
                _stateMachine.SetState(_runToSoundState);
            }
        }


        public PatrolState GetPatrolState()
        {
            return _patrolState;
        }
        public void GetChaseState()
        {
            _stateMachine.SetState(_chaseState);
        }

        public EnemyPickupState GetPickup()
        {
            return _enemyPickupState;
        }
        public EnemyPickupPlayerState GetPickupPlayer()
        {
            return _enemyPickupPlayerState;
        }

        public void TrashPickup()
        {
            Debug.Log("TrashPickup event triggered!");
            _enemyPickupState?.HandleTrashPickup();
        }

        public void PlayerPickup()
        {
            Debug.Log("PlayerPickup event triggered!");
            _enemyPickupPlayerState?.HandlePlayerPickup();
        }
        
        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }
    }
}
