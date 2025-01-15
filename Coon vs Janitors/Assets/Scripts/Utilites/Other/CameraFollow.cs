using UnityEngine;

namespace Raccons_House_Games
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
        private Vector3 _velocity = Vector3.zero;

        private Vector3 initialRotation;

        private void Start()
        {
            initialRotation = transform.eulerAngles;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
            transform.eulerAngles = initialRotation;
        }
    }
}
