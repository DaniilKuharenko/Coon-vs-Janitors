using Ability.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityStorage : MonoBehaviour
    {
        [SerializeField] private AbilityConfigs[] _abilityConfigs;
        [SerializeField] private Actor _owner;

        private List<Ability> _abilities = new();

        public void Init()
        {
            for (int i = 0; i < _abilityConfigs.Length; ++i)
            {
                var builder = _abilityConfigs[i].GetBuilder();

                builder.Make();
                var ability = builder.GetResult();

                ability.Added(_owner);


                _abilities.Add(ability);
            }
        }

        public Ability[] GetAbilities() => _abilities.ToArray();

    }
}