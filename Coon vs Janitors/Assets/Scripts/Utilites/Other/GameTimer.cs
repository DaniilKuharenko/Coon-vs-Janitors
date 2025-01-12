using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Raccons_House_Games
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private Image _circleFill;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private int _duration;
        private int _remainingDuration;

        private void Start() // temporary use of Start later it will need to be removed
        {

        }

        private void Begin(int second)
        {

        }
    }
}
