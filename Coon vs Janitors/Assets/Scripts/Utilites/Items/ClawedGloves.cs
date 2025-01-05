using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class ClawedGloves : MonoBehaviour
    {
        [SerializeField] private Button _useGlovesButton;
        [SerializeField] private GameObject _useGlovesButtonUI;
        private GarbageCanController[] _garbageCanControllers;

        public void InitializeGloves()
        {
            _garbageCanControllers = FindObjectsByType<GarbageCanController>(FindObjectsSortMode.None);
            if(_useGlovesButton != null)
            {
                _useGlovesButton.onClick.AddListener(TestUseButton);
            }
        }

        private void TestUseButton()
        {
            if (_garbageCanControllers != null)
            {
                foreach (var garbageCanController in _garbageCanControllers)
                {
                    garbageCanController.ChangeInteractionTime(1f);
                }
            }
        }
    }
}
