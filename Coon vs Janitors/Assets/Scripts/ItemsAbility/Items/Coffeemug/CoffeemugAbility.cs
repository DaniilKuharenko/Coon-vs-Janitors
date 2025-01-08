using Items;
using UnityEngine;

namespace Raccons_House_Games
{
    public class CoffeemugAbility : AbilityItem
    {
        public float SpeedMultiplier { get; private set; }
        public float Duration { get; private set; }
        private float _elapsedTime;
        private bool _isEffectActive;
        private Actor _owner;
        private PlayerControll _config;

        public CoffeemugAbility(float speedMultiplier, float duration, Actor owner, PlayerControll config)
        {
            _config = config;
            SpeedMultiplier = speedMultiplier;
            Duration = duration;
            _elapsedTime = 0.0f;
            _isEffectActive = false;
            _owner = owner;
        }

        public override void OnUse()
        {
            Debug.LogError("ON Use Activated");
            if (!_isEffectActive)
            {
                Debug.LogError("Effect is not active, applying effect...");
                ApplyEffect();
            }
        }

        public override void ApplyEffect()
        {
            if (_config != null)
            {
                _config.SetSpeedMultiplier(SpeedMultiplier);
                Debug.LogError($"Speed: {SpeedMultiplier}");
                _isEffectActive = true;
                _elapsedTime = 0.0f;
            }
            else
            {
                Debug.LogError("PlayerControll not found in CoffeemugAbilityConfig!");
            }
        }

        public override void EventTick(float deltaTick)
        {
            if (_isEffectActive)
            {
                _elapsedTime += deltaTick;
                if (_elapsedTime >= Duration)
                {
                    CancelUse();
                }
            }
        }

        public override void CancelUse()
        {
            _isEffectActive = false;
        }
    }
}