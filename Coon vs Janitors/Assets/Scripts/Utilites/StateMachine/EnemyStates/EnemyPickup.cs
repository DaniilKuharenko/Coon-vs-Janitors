using UnityEngine;

namespace Raccons_House_Games
{

    public class EnemyPickup : IState
    {
        private readonly EnemyControll _enemyControl;
        private readonly Animator _animator;
        private readonly Transform _holdPoint;
        private readonly GameObject _pickupedItem;

        public EnemyPickup(EnemyControll enemyControll, Animator animator, 
            Transform holdPoint, GameObject pickupedItem){
            
            _enemyControl = enemyControll;
            _animator = animator;
            _holdPoint = holdPoint;
            _pickupedItem = pickupedItem;

        }

        public void OnEnter()
        {
            Debug.Log("Item Pickup");
        }
        
        public void Update()
        {

        }

        public void OnExit()
        {
            Debug.Log("Item Droped");
        }

    }

}