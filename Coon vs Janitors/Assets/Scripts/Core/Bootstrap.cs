using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _loadingScreen;
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 600;
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            _loadingScreen.SetActive(true);

            yield return new WaitForSeconds(4.2f);
            _gameManager.StartGame();

            _loadingScreen.SetActive(false);
        }
    }
}
