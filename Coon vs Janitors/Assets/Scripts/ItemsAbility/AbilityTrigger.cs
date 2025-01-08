using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityTrigger : MonoBehaviour
    {
        [SerializeField] private AbilityItem _abilityItem;

        public AbilityItem GetAbilityItem() => _abilityItem;
    }
}
