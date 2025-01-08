using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityItemHandler : MonoBehaviour, IInjectServices
    {
        [SerializeField] private AbilityItemStorage _abilityItemStorage;
        [SerializeField] private Actor _ownerActor;
        private List<AbilityItem> _abilityItems = new();
        private AbilityItem _currentAbilityItem;

        public void Inject(IServiceLocator locator)
        {
            _abilityItemStorage.Init();
            _abilityItems.AddRange(_abilityItemStorage.GetAbilityItems());
            // at future need hook into UI or other components
        }

        public void OnSelectAbilityItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _abilityItems.Count)
            {
                Debug.LogError($"Invalid index {itemIndex}. List count is {_abilityItems.Count}.");
                return;
            }

            _currentAbilityItem?.CancelUse();
            switch(_abilityItems[itemIndex].Status)
            {
                case EItemStatus.Ready:
                    _currentAbilityItem = _abilityItems[itemIndex];
                    _currentAbilityItem.OnEquip(_ownerActor);
                    Debug.LogWarning($"Current ability item set to: {_currentAbilityItem.GetType().Name}");
                    _currentAbilityItem.OnUse();
                    break;

                case EItemStatus.Expired:
                    Debug.LogWarning("Attempted to select an expired item.");
                    break;
            }
        }



        private void Update()
        {
            for(int i = 0; i < _abilityItems.Count; i++)
            {
                _abilityItems[i].EventTick(Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.LogError($"Triggered by: {other.gameObject.name}");

            if (_abilityItems.Count == 0)
            {
                Debug.LogError("Ability items list is empty! Can't select an ability item.");
                return;
            }

            Vector3 location = other.transform.position;
            Actor target = other.GetComponent<Actor>();

            if (other.CompareTag("Player"))
            {
                OnSelectAbilityItem(0);
                if (_currentAbilityItem != null && _currentAbilityItem.CheckCondition(_ownerActor, target, location))
                {
                    Debug.LogError("Condition met, applying effect.");
                }
            }
        }

        private void OnDestroy(){}
    }
}