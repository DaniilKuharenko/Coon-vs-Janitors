using Items;
using UnityEngine;

namespace Raccons_House_Games
{
    public class CoffeemugAbilityBuilder : AbilityItemBuilder
    {
        private readonly CoffeemugAbilityConfig _coffeemugAbilityConfig;
        private readonly Actor _actor;

        public CoffeemugAbilityBuilder(CoffeemugAbilityConfig config) : base (config)
        {
            _coffeemugAbilityConfig = config;
        }

        public override void Make()
        {
            _abilityItem = new CoffeemugAbility(_coffeemugAbilityConfig.SpeedMultiplier, _coffeemugAbilityConfig.Duration, _actor);
            base.Make();
        }
    }
}