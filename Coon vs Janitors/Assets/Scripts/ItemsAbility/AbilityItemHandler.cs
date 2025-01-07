using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityItemHandler : MonoBehaviour, IInjectServices
    {
        [SerializeField] private AbilityItemStorage _abilityItemStorage;
        private List<AbilityItem> _abilityItems = new();

        public void Inject(IServiceLocator locator)
        {
            _abilityItemStorage.Init();
            _abilityItems.AddRange(_abilityItemStorage.GetAbilityItems());
            
            // at future need hook into UI or other components
        }
    }
}