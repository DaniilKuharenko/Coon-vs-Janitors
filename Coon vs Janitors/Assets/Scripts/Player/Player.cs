using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Player : MonoBehaviour
    {
        public string TrashScore { get; private set; }
        private List<AbilityItem> _activeAbilities = new List<AbilityItem>();
        private AbilityItem _passiveAbility;
    }
}
