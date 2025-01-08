using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityItemHandler : MonoBehaviour //, IInjectServices
    {
        [SerializeField] private List<AbilityItem> _abilityItems = new();
        private AbilityItem _currentAbilityItem;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out AbilityTrigger abilityTrigger))
            {
                _currentAbilityItem = abilityTrigger.GetAbilityItem();
                if (_currentAbilityItem != null)
                {
                    _currentAbilityItem.OnUse();
                    _currentAbilityItem = null; // Reset the current item cuz the effect is a one-time effect
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out AbilityTrigger abilityTrigger))
            {
                AbilityItem exitingItem = abilityTrigger.GetAbilityItem();
                if (exitingItem != null && exitingItem == _currentAbilityItem)
                {
                    _currentAbilityItem?.OnUnequip(null);
                }
            }
        }
    }
}
