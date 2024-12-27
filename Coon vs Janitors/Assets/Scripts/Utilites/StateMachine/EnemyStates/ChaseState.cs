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

        private bool _isAnimationComplete;
        private float _animationDuration = 0.6f; // Animation duration (in seconds)
        private float _animationTimer;
        private float _originalSpeed;
        private float _runSpeedMultiplier = 1.2f; // Speed multiplier for running
        private float _maxSpeed = 10.0f; // Maximum allowed speed
        protected static readonly int ChaseHash = Animator.StringToHash("ChaseStart");
        protected static readonly int RunningHash = Animator.StringToHash("Run");

        public ChaseState(EnemyControll enemyControl, Animator animator, NavMeshAgent agent, StateMachine stateMachine)
        {
            _enemyControl = enemyControl;
            _animator = animator;
            _agent = agent;
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            Debug.Log("Chase Start");

            _isAnimationComplete = false;
            _animationTimer = _animationDuration;

            _originalSpeed = _agent.speed;

            _agent.enabled = false;
            _animator.CrossFade(ChaseHash, 0.1f);
        }

        public void Update()
        {
            Debug.Log("Chasing");

            if (!_isAnimationComplete)
            {
                // Delay while the animation is running
                _animationTimer -= Time.deltaTime;
                if (_animationTimer <= 0f)
                {
                    _isAnimationComplete = true;
                    _agent.enabled = true;

                    // Switch to the Run animation
                    _animator.CrossFade(RunningHash, 0.1f);

                    // Increase the enemy speed for running, limited by max speed
                    _agent.speed = Mathf.Min(_originalSpeed * _runSpeedMultiplier, _maxSpeed);
                }
                return;
            }

            if (_enemyControl.Target != null)
            {
                _agent.destination = _enemyControl.Target.position;
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

            // attack radius
            if (Vector3.Distance(_enemyControl.transform.position, _enemyControl.Target.position) < 2f)
            {
                if (_enemyControl.Target.CompareTag("Pickup"))
                {
                    _stateMachine.SetState(_enemyControl.GetPickup());
                }

                if (_enemyControl.Target.CompareTag("Player"))
                {
                    _stateMachine.SetState(_enemyControl.GetPickupPlayer());
                }
            }
        }
    }
}
