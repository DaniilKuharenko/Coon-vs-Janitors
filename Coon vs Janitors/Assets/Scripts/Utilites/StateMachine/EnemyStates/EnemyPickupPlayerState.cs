using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{

    public class EnemyPickupPlayerState : IState
    {
        private readonly EnemyControll _enemyControl;
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Transform _holdPoint;
        private readonly Transform _shedPoint;
        private GameObject _pickupedPlayer;
        private StateMachine _stateMachine;
        protected static readonly int PickupHash = Animator.StringToHash("PickingUp");
        protected const float crossFadeDuration = 0.1f;

        private bool isPickup = false;

        public EnemyPickupPlayerState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, 
            Transform holdPoint, Transform shedPoint, 
            StateMachine stateMachine) {
            
            _enemyControl = enemyControll;
            _animator = animator;
            _agent = agent;
            _holdPoint = holdPoint;
            _shedPoint = shedPoint;
            _stateMachine = stateMachine;

        }

        public void OnEnter()
        {
            Debug.Log("Player Pickup");

            _pickupedPlayer= _enemyControl.Target?.gameObject;
            if (_pickupedPlayer== null)
            {
                Debug.LogError("No player to pick up! Target is null or not set.");
            }

            _animator.speed = 4.0f;
            _animator.CrossFade(PickupHash, crossFadeDuration);
        }
        
        public void Update()
        {
            if(isPickup)
            {
                MoveToShed();

                if(!_agent.pathPending && _agent.remainingDistance <= 0.5)
                {
                    Debug.Log("Trash point reached!");
                    DropItem();
                }
            }
        }

        public void OnExit()
        {
            Debug.Log("PlayerDroped");
            _animator.speed = 1.0f;
        }
        
        public void HandlePlayerPickup()
        {
            PlayerPickup();
        }

        private void PlayerPickup()
        {
            if (_pickupedPlayer== null) return;

            Rigidbody rb = _pickupedPlayer.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            _pickupedPlayer.transform.SetParent(_holdPoint);
            _pickupedPlayer.transform.localPosition = Vector3.zero;
            isPickup = true;
        }

        private void MoveToShed()
        {
            if(!isPickup) return;

            _agent.destination = _shedPoint.position;
        }

        private void DropItem()
        {
            Debug.Log("DropPlayeris called");
            if (_pickupedPlayer== null) return;

            if (_pickupedPlayer!= null)
            {
                Rigidbody rb = _pickupedPlayer.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    
                    // Here Vector3.down instead of transform.down because transform is part of MonoBehaviour and is only available in classes that inherit from MonoBehaviour.
                    rb.AddForce(Vector3.down * 10.0f, ForceMode.Impulse);
                }

                _pickupedPlayer.transform.SetParent(null);
                _pickupedPlayer= null;

                isPickup = false;
                Debug.Log("Subject dropped.");

                _enemyControl.SetTarget(null);
                _stateMachine.SetState(_enemyControl.GetPatrolState());
            }
        }
    }

}