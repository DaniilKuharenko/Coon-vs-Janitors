using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    [CreateAssetMenu(menuName = "CoonGame/Abilities/Coffeemug", fileName = "CoffeemugConfig")]
    public class CoffeemugAbilityConfig : AbilityItemConfig
    {
        [field: SerializeField] public float SpeedMultiplier { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }

        public override AbilityItemBuilder GetBuilder()
        {
            return new CoffeemugAbilityBuilder(this);
        }
    }
}