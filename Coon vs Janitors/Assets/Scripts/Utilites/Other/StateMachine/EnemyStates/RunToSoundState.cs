using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{
    public class RunToSoundState : IState
    {
        private readonly EnemyControll _enemyControll;
        private readonly StateMachine _stateMachine;
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        private readonly float _distanceToSound;
        private Transform _closestSoundSource;

        private static readonly int LoockHash = Animator.StringToHash("LookingDown");
        private static readonly int RunnHash = Animator.StringToHash("Run");
        private const float crossFadeDuration = 0.1f;

        private float _waitTimer;
        private bool _isWaiting;
        private float _waitTime = 2.0f;

        public RunToSoundState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, StateMachine stateMachine, Transform transform, float distanceToSound)
        {
            _enemyControll = enemyControll;
            _animator = animator;
            _agent = agent;
            _stateMachine = stateMachine;
            _transform = transform;
            _distanceToSound = distanceToSound;
        }

        public void OnEnter()
        {
            Debug.Log("Run Start");
            _animator.CrossFade(RunnHash, 0.1f);
        }

        public void Update()
        {
            if (_transform == null)
            {
                Debug.LogError("Transform is not set!");
                return;
            }

            Debug.Log("Running");

            Collider[] detectedObjects = Physics.OverlapSphere(_transform.position, _distanceToSound);
            _closestSoundSource = null;
            float closestDistance = float.MaxValue;

            foreach (var obj in detectedObjects)
            {
                if (obj.CompareTag("Pickup"))
                {
                    float distance = Vector3.Distance(_transform.position, obj.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        _closestSoundSource = obj.transform;
                    }
                }
            }

            if (_closestSoundSource != null)
            {
                _agent.destination = _closestSoundSource.position;
                CheckPlace();
            }
        }

        public void OnExit()
        {
            Debug.Log("Run Exit");
            _agent.isStopped = false;
        }

        private void CheckPlace()
        {
            if (!_agent.pathPending && _agent.remainingDistance < 2.0f)
            {
                if (!_isWaiting)
                {
                    _animator.CrossFade(LoockHash, crossFadeDuration);
                    _agent.isStopped = true;
                    _isWaiting = true;
                }

                _waitTimer += Time.deltaTime;

                if (_waitTimer >= _waitTime)
                {
                    _waitTimer = 0f;
                    _isWaiting = false;
                    _stateMachine.SetState(_enemyControll.GetPatrolState());
                }
            }
        }
    }
}


