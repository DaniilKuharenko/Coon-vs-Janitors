using UnityEngine;
using TMPro;

namespace Raccons_House_Games
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fpsText;
        private float _deltaTime;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            _fpsText.text = $"FPS: {fps:0.}";
        }
    }
}
