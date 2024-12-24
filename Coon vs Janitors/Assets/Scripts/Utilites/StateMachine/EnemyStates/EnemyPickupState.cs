using UnityEngine;
using UnityEngine.AI;

namespace Raccons_House_Games
{

    public class EnemyPickupState : IState
    {
        private readonly EnemyControll _enemyControl;
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Transform _holdPoint;
        private readonly Transform _trashPoint;
        private GameObject _pickupedItem;
        private StateMachine _stateMachine;
        private TrashController _trashController;
        protected static readonly int PickupHash = Animator.StringToHash("PickingUp");
        protected const float crossFadeDuration = 0.1f;

        private bool isPickup = false;

        public EnemyPickupState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, 
            Transform holdPoint, Transform trashPoint, 
            StateMachine stateMachine, TrashController trashController){
            
            _enemyControl = enemyControll;
            _animator = animator;
            _agent = agent;
            _holdPoint = holdPoint;
            _trashPoint = trashPoint;
            _stateMachine = stateMachine;
            _trashController = trashController;

        }

        public void OnEnter()
        {
            Debug.Log("Item Pickup");

            _pickupedItem = _enemyControl.Target?.gameObject;
            if (_pickupedItem == null)
            {
                Debug.LogError("No item to pick up! Target is null or not set.");
            }

            _animator.CrossFade(PickupHash, crossFadeDuration);
        }
        
        public void Update()
        {
            if(isPickup)
            {
                MoveToTrash();

                if(!_agent.pathPending && _agent.remainingDistance <= 0.5)
                {
                    Debug.Log("Trash point reached!");
                    DropItem();
                }
            }
        }

        public void OnExit()
        {
            Debug.Log("Item Droped");
        }
        
        public void HandleTrashPickup()
        {
            TrashPickup();
        }

        private void TrashPickup()
        {
            if (_pickupedItem == null) return;

            Rigidbody rb = _pickupedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            _pickupedItem.transform.SetParent(_holdPoint);
            _pickupedItem.transform.localPosition = Vector3.zero;
            isPickup = true;
        }

        private void MoveToTrash()
        {
            if(!isPickup) return;

            _agent.destination = _trashPoint.position;
        }

        private void DropItem()
        {
            Debug.Log("DropItem is called");
            if (_pickupedItem == null) return;

            if (_pickupedItem != null)
            {
                Rigidbody rb = _pickupedItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    // Here Vector3.down instead of transform.down because transform is part of MonoBehaviour and is only available in classes that inherit from MonoBehaviour.
                    rb.AddForce(Vector3.down * 10.0f, ForceMode.Impulse);
                }

                _pickupedItem.transform.SetParent(null);

                _trashController.ReturnTrashToPool(_pickupedItem);

                _pickupedItem = null;
                isPickup = false;
                Debug.Log("Subject dropped.");

                _enemyControl.SetTarget(null);
                _stateMachine.SetState(_enemyControl.GetPatrolState());
            }
        }
    }

}