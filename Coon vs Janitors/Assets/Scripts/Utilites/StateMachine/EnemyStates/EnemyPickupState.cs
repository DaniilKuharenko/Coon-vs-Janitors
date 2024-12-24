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

        private bool isPickup = false;

        public EnemyPickupState(EnemyControll enemyControll, Animator animator, NavMeshAgent agent, 
            Transform holdPoint, Transform trashPoint, StateMachine stateMachine){
            
            _enemyControl = enemyControll;
            _animator = animator;
            _agent = agent;
            _holdPoint = holdPoint;
            _trashPoint = trashPoint;
            _stateMachine = stateMachine;

        }

        public void OnEnter()
        {
            Debug.Log("Item Pickup");
            TrashPickup();
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
                    isPickup = false;
                    _stateMachine.SetState(_enemyControl.GetPatrolState());
                }
            }
        }

        public void OnExit()
        {
            Debug.Log("Item Droped");
        }

        private void TrashPickup()
        {

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

        }
    }

}