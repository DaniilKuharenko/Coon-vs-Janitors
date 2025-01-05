using UnityEngine;

namespace Raccons_House_Games
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ClawedGloves _clawedGloves;

        private void Awake()
        {
            _clawedGloves.InitializeGloves();
        }
    }
}
