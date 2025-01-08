using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    [CreateAssetMenu(menuName = "CoonGame/Abilities/Coffeemug", fileName = "CoffeemugConfig")]
    public class CoffeemugAbilityConfig : AbilityItemConfig
    {
        [field: SerializeField] public float SpeedMultiplier { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        public PlayerControll PlayerControll { get; private set; }

        public override AbilityItemBuilder GetBuilder()
        {
            return new CoffeemugAbilityBuilder(this);
        }

        public PlayerControll FindPlayerControll()
        {
            PlayerControll = FindFirstObjectByType<PlayerControll>();
            if (PlayerControll == null)
            {
                Debug.LogError("PlayerControll not found in the scene!");
            }
            return PlayerControll;
        }
    }
}