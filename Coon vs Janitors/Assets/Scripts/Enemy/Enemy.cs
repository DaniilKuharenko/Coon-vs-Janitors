using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 5.0f;
        [SerializeField] private float _pickupRadius = 2.0f;
        [SerializeField] private float _circleHeight = 0f; 
        [SerializeField] private LayerMask _targetLayer;

        private Transform _target;

        private void Update()
        {
            OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            DrawCircle(transform.position + Vector3.up * _circleHeight, _detectionRadius, Color.green);

            DrawCircle(transform.position + Vector3.up * _circleHeight, _pickupRadius, Color.yellow);
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
