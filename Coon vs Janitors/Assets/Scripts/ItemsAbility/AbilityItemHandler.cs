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
            _currentAbilityItem?.OnUnequip(_ownerActor);

            switch(_abilityItems[itemIndex].Status)
            {
                case EItemStatus.Ready:
                    _currentAbilityItem = _abilityItems[itemIndex];
                    _currentAbilityItem.OnEquip(_ownerActor);
                break;

                case EItemStatus.Expired:
                break;
            }
        }

        private void Update()
        {
            for(int i = 0; i < _abilityItems.Count; i++)
            {
                _abilityItems[i].EventTick(Time.deltaTime);
            }

            if(_currentAbilityItem != null)
            {
                Vector3 location = Vector3.zero;
                Actor target = null;
                
                if (_currentAbilityItem.CheckCondition(_ownerActor, target, location))
                {
                    _currentAbilityItem.ApplyEffect();
                    _currentAbilityItem = null;
                }
            }
        }

        private void OnDestroy(){}
    }
}