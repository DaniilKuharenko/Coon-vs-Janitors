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
            foreach (var config in _itemConfigs)
            {
                var builder = config.GetBuilder();
                builder.Make();
                var item = builder.GetResult();
                _abilityItems.Add(item);
                Debug.LogError($"Ability item added: {item.GetType().Name}");
            }
        }


        public AbilityItem[] GetAbilityItems() => _abilityItems.ToArray();
    }
}