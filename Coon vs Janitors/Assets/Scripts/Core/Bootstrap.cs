using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        private GpuInctancingEnabler _gpuInctancingEnabler;
        private void Awake()
        {
            _gpuInctancingEnabler = new GpuInctancingEnabler();
            _gpuInctancingEnabler.GpuInctancEnable();
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            yield return new WaitForSeconds(1.2f);
            _gameManager.StartGame();
        }
    }
}
