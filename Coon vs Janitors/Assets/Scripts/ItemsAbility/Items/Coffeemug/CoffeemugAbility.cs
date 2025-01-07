using UnityEngine;

namespace Raccons_House_Games
{
    public class CoffeemugAbility : AbilityItem
    {
        public float SpeedBoost { get; private set; }

        public CoffeemugAbility(float speedBoost)
        {
            SpeedBoost = speedBoost;
        }
    }
}
