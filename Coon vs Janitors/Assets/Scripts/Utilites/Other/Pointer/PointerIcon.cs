using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class PointerIcon : MonoBehaviour 
    {
        [SerializeField] Image _image;
        bool _isShown = true;

        public void InitializePointerIcon() 
        {
            _image.enabled = false;
            _isShown = false;
        }

        public void SetIconPosition(Vector3 position, Quaternion rotation) 
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Show() 
        {
            if (_isShown)
            {
                return;
            }
            
            _isShown = true;
            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }

        public void Hide() 
        {
            if (!_isShown)
            {
                return;
            }

            _isShown = false;

            StopAllCoroutines();
            StartCoroutine(HideProcess());
        }

        IEnumerator ShowProcess() 
        {
            _image.enabled = true;
            transform.localScale = Vector3.zero;

            for (float t = 0; t < 1f; t += Time.deltaTime * 4f) 
            {
                transform.localScale = Vector3.one * t;
                yield return null;
            }

            transform.localScale = Vector3.one;
        }

        IEnumerator HideProcess() 
        {
            for (float t = 0; t < 1f; t += Time.deltaTime * 4f) 
            {
                transform.localScale = Vector3.one * (1f - t);
                yield return null;
            }
            _image.enabled = false;
        }

    }
}
