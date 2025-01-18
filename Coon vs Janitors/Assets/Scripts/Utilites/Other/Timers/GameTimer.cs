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
        private bool _pause = false;
        private Coroutine _timerCoroutine;

        private void Start() // Temporarily using Start, can be removed later
        {
            StartTimer(_duration);
        }

        public void StartTimer(int seconds)
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            _timerCoroutine = StartCoroutine(TimerCoroutine(seconds));
        }

        private IEnumerator TimerCoroutine(int seconds)
        {
            _remainingDuration = seconds;
            while (_remainingDuration >= 0) // Counts down to 0
            {
                if (!_pause)
                {
                    UpdateTimerUI();
                    _remainingDuration--;
                }
                yield return new WaitForSeconds(1f);
            }
            OnEndTime();
        }

        private void UpdateTimerUI()
        {
            _timerText.text = $"{_remainingDuration / 60:00} : {_remainingDuration % 60:00}";
            _circleFill.fillAmount = Mathf.InverseLerp(0, _duration, _remainingDuration);
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
