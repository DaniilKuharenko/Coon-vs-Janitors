using UnityEngine;

namespace Raccons_House_Games
{
    public class BoxInAss : MonoBehaviour
    {
        [SerializeField] private Transform _boxTransform;

        // Model link for visual transform
        [SerializeField] private Transform _modelBody;

        [SerializeField] private Vector3 _scaleDown = new Vector3(1.2f, 0.8f, 1.2f);
        [SerializeField] private Vector3 _scaleUp = new Vector3(0.8f, 1.2f, 0.8f);
        [SerializeField] private float _scaleCoefficient;

        void Update()
        {
            Vector3 _realitivePosition = _boxTransform.InverseTransformPoint(transform.position);
            float _interpolate = _realitivePosition.y * _scaleCoefficient;
            Vector3 _scale = Lerp3(_scaleDown, Vector3.one, _scaleUp, _interpolate);
            _modelBody.localScale = _scale;
        }

        Vector3 Lerp3(Vector3 _paramA, Vector3 _paramB, Vector3 _paramC, float _paramT)
        {
            if (_paramT < 0)
                return Vector3.LerpUnclamped(_paramA, _paramB, _paramT + 1f);
            else
                return Vector3.LerpUnclamped(_paramB, _paramC, _paramT);
        }
    }
}
