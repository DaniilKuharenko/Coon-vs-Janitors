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

        public CoffeemugAbility(float speedMultiplier, float duration)
        {
            SpeedMultiplier = speedMultiplier;
            Duration = duration;
            _elapsedTime = 0.0f;
            _isEffectActive = false;
        }

        public override void OnEquip(Actor owner)
        {
            if(owner.TryGetComponent(out PlayerControll playerControll))
            {
                playerControll.SetSpeedMultiplier(SpeedMultiplier);
                _isEffectActive = true;
                _elapsedTime = 0.0f;
            }
        }

        public override void EventTick(float deltaTick)
        {
            if(_isEffectActive)
            {
                _elapsedTime += deltaTick;
                if(_elapsedTime >= Duration)
                {
                    ResetEffect();
                }
            }
        }

        private void ResetEffect()
        {
            if (TryGetOwner(out PlayerControll playerControll))
            {
                playerControll.SetSpeedMultiplier(1.0f);
                _isEffectActive = false;
            }
        }

        public override void OnUnequip(Actor owner)
        {
            if(owner.TryGetComponent(out PlayerControll playerControll))
            {
                playerControll.SetSpeedMultiplier(1.0f);
            }
            _isEffectActive = false;
        }

        private bool TryGetOwner(out PlayerControll playerControll)
        {
            playerControll = null;
            return playerControll != null;
        }
    }
}
