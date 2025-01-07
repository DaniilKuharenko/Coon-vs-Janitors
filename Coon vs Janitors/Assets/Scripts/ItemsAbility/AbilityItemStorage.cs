using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityItemStorage : MonoBehaviour
    {
        [SerializeField] private AbilityItemConfig[] _itemConfigs;
        private List<AbilityItem> _abilityItems = new();

        public void Init()
        {
            foreach(var config in _itemConfigs)
            {
                var builder = config.GetBuilder();
                builder.Make();
                _abilityItems.Add(builder.GetResult());
            }
        }

        public AbilityItem[] GetAbilityItems() => _abilityItems.ToArray();
    }
}