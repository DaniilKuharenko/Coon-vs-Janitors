using UnityEngine;

namespace Raccons_House_Games
{
    public class EnemyControll : Enemy
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        private Transform _target;

        private void Update()
        {
            DetectTargets();
            if(_target != null)
            {
                MoveTowardsTarget();
                CheckPickup();
            }
        }

        // Target detection in the horizontal plane (XZ)
        private void DetectTargets()
        {
            Collider[] detectedObjects = Physics.OverlapSphere(transform.position, DetectionRadius, TargetLayer);
            foreach(var detected in detectedObjects)
            {
                Vector3 horizontalDistance = new Vector3(
                    detected.transform.position.x - transform.position.x,
                    0,
                    detected.transform.position.z - transform.position.z
                );

                float heightDifference = Mathf.Abs(detected.transform.position.y - (transform.position.y + CircleHeight));
                if (horizontalDistance.magnitude <= DetectionRadius && heightDifference <= 1f)
                {
                    _target = detected.transform;
                    return;
                }
            }

            _target = null;
        }

        // Moving towards the goal
        private void MoveTowardsTarget()
        {
            if (_target == null) return;

            Vector3 direction = new Vector3(
                _target.position.x - transform.position.x,
                0,
                _target.position.z - transform.position.z
            ).normalized;

            transform.position += direction * _moveSpeed * Time.deltaTime;
        }

        // Check the pickup radius
        private void CheckPickup()
        {
            if (_target == null) return;

            float horizontalDistance = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(_target.position.x, _target.position.z)
            );

            float heightDifference = Mathf.Abs(_target.position.y - (transform.position.y + CircleHeight));

            if (horizontalDistance <= PickupRadius && heightDifference <= 1f) // Adjusting the hit height
            {
                Debug.Log("Hit");
            }
        }
    }
}
