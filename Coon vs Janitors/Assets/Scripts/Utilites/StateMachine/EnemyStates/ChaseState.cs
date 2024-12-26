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
        private float _animationDuration = 1.5f; // Animation duration (in seconds)
        private float _animationTimer;
        protected static readonly int ChaseHash = Animator.StringToHash("ChaseStart");

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

            _isAnimationComplete = false;
            _animationTimer = _animationDuration;

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
                }
                return;
            }

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
