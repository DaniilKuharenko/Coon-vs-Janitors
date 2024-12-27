using UnityEngine;

namespace Raccons_House_Games
{
    public class Enemy : MonoBehaviour
    {
        [Header("Field of View Parameters")]
        [SerializeField] private LayerMask _visionObstructingLayer;
        [SerializeField] private float _visionRange = 10f;
        [SerializeField] private float _visionAngle = 90f;
        [SerializeField] private int _visionConeResolution = 120;
        [SerializeField] private float _rayHeight = 0.6f;
        [SerializeField] private float _detectionRadius = 5f;
        [SerializeField] private float _circleHeight = 0f;

        public float VisionAngle => _visionAngle;
        public float VisionRange => _visionRange;
        public float DetectionRadius => _detectionRadius;
        public LayerMask VisionObstructingLayer => _visionObstructingLayer;

        private void OnDrawGizmos()
        {
            DrawVisionCone();
        }

        private void DrawVisionCone()
        {
            int segments = _visionConeResolution;
            float angleStep = _visionAngle / segments;

            float currentAngle = -_visionAngle / 2;

            // Get the current character rotation (Y-axis)
            Quaternion rotation = transform.rotation * Quaternion.Euler(0, -90, 0);

            for (int i = 0; i < segments; i++)
            {
                float radianAngle = currentAngle * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

                // Account for the character's rotation by adding an offset
                direction = rotation * direction;

                Vector3 rayOrigin = transform.position + Vector3.up * _rayHeight;

                RaycastHit hit;

                // Run Raycast to determine the distance
                if (Physics.Raycast(rayOrigin, direction, out hit, _visionRange, _visionObstructingLayer))
                {
                    // Draw the ray to the collision point
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(rayOrigin, hit.point);
                }
                else
                {
                    // Draw the ray up to the maximum range
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(rayOrigin, rayOrigin + direction * _visionRange);
                }

                currentAngle += angleStep;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            DrawCircle(transform.position + Vector3.up * _circleHeight, _detectionRadius, Color.yellow);;
        }

        private void DrawCircle(Vector3 center, float radius, Color color)
        {
            int segments = 64;
            float angleStep = 360f / segments;

            Vector3 previousPoint = center + new Vector3(radius, 0, 0);

            Gizmos.color = color;

            for (int i = 1; i <= segments; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
        }
    }
}
