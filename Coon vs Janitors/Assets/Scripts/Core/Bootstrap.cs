using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 600;
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            yield return new WaitForSeconds(1.2f);
            _gameManager.StartGame();
        }
    }
}
