using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityItemHandler : MonoBehaviour
    {
        [SerializeField] private AbilityItemStorage _itemStorage;
        private List<AbilityItem> _abilityItems = new();
        private AbilityItem _currentAbilityItem;

        private void Start()
        {
            _itemStorage.Init();
            _abilityItems.AddRange(_itemStorage.GetAbilityItems());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _currentAbilityItem == null)
            {
                Debug.LogError("Trigerr work");
                foreach (var item in _abilityItems)
                {
                    if (item.CanBeActivated())
                    {
                        Debug.LogError("Trigerr work 2");
                        _currentAbilityItem = item;
                        _currentAbilityItem.OnUse();
                        break;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && _currentAbilityItem != null)
            {
                _currentAbilityItem = null;
            }
        }
    }
}