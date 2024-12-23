using UnityEngine;

namespace Raccons_House_Games
{
    public class EnemyControll : Enemy
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private Animator _animator;
        private Transform _target;
        private StateMachine _stateMachine;
        private EnemyIdleState _idleState;
        private EnemyWalkState _walkState;
        private float _checkSpeed;

        public void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState = new EnemyIdleState(this, _animator);
            _walkState = new EnemyWalkState(this, _animator);
            
            _stateMachine.AddTransition(_idleState, _walkState, new Predicate(() => _checkSpeed >= 0.5));
            _stateMachine.AddTransition(_walkState, _idleState, new Predicate(() => _checkSpeed <= 0.5));

            _stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _stateMachine?.Update();
            
            DetectTargets();
            CheckingSpeed();

            if(_target != null)
            {
                MoveTowardsTarget();
                CheckPickup();
            }
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
            Collider[] detectedObjects = Physics.OverlapSphere(transform.position, DetectionRadius, TargetLayer);
            foreach(var detected in detectedObjects)
            {
                Vector3 horizontalDistance = new Vector3(
                    detected.transform.position.x - transform.position.x,
                    0,
                    detected.transform.position.z - transform.position.z
                );

                float heightDifference = Mathf.Abs(detected.transform.position.y - (transform.position.y + CircleHeight));
                if (horizontalDistance.magnitude <= DetectionRadius && heightDifference <= 1f)
                {
                    _target = detected.transform;
                    return;
                }
            }

            _target = null;
        }

        // Moving towards the goal
        private void MoveTowardsTarget()
        {
            if (_target == null) return;

            Vector3 direction = new Vector3(
                _target.position.x - transform.position.x,
                0,
                _target.position.z - transform.position.z
            ).normalized;

            transform.position += direction * _moveSpeed * Time.deltaTime;
        }

        // Check the pickup radius
        private void CheckPickup()
        {
            if (_target == null) return;

            float horizontalDistance = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(_target.position.x, _target.position.z)
            );

            float heightDifference = Mathf.Abs(_target.position.y - (transform.position.y + CircleHeight));

            if (horizontalDistance <= PickupRadius && heightDifference <= 1f) // Adjusting the hit height
            {
                Debug.Log("Hit");
            }
        }
    }
}
