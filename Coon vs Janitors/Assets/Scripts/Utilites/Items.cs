using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private AudioClip _collisionSound;
        [SerializeField] private float _radiusSound = 5.0f;
        [SerializeField] private LayerMask _enemyLayer;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void SoundPlay()
        {
            //Play Sound
            if(_audioSource != null && _collisionSound != null)
            {
                _audioSource.PlayOneShot(_collisionSound);
            }

            //Enemy Alert
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radiusSound, _enemyLayer);
            if (hitColliders.Length > 0)
            {
                AlertEnemys();
            }
        }

        private void AlertEnemys()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radiusSound, _enemyLayer);
            foreach(var hitCollider in hitColliders)
            {
                EnemyControll enemy = hitCollider.GetComponent<EnemyControll>();
                if(enemy != null)
                {
                    enemy.Alert(transform.position);
                }
            } 
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            DrawWireCircle(transform.position, _radiusSound, Vector3.up);
        }

        private void DrawWireCircle(Vector3 position, float radius, Vector3 up)
        {
            int segments = 64;
            float angleStep = 360f / segments;
            Vector3 prevPoint = position + Quaternion.Euler(0, 0, 0) * Vector3.forward * radius;

            for (int i = 1; i <= segments; i++)
            {
                float angle = i * angleStep;
                Vector3 nextPoint = position + Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}
