using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            yield return new WaitForSeconds(1.2f);
        }
    }
}
