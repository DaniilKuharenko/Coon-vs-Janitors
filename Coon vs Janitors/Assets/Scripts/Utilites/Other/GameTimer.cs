using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Raccons_House_Games
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private Image _circleFill;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private int _duration;
        private int _remainingDuration;
        private bool _pause;

        private void Start() // temporary use of Start later it will need to be removed
        {
            Begin(_duration);
        }

        private void Begin(int second)
        {
            _remainingDuration = second;
            StartCoroutine(TimerTick());
        }

        private IEnumerator TimerTick()
        {
            while(_remainingDuration >= 0)
            {
                if(!_pause)
                {
                    _timerText.text = $"{_remainingDuration / 60:00} : {_remainingDuration % 60:00}";
                    _circleFill.fillAmount = Mathf.InverseLerp(0, _duration, _remainingDuration);
                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
            OnEndTime();
        }

        private void OnEndTime()
        {
            Debug.Log("Timer End");
        }

        public void Pause()
        {
            _pause = !_pause;
        }
    }
}
