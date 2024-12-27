using UnityEngine;

namespace Raccons_House_Games
{
    public class Items : MonoBehaviour
    {   
        [SerializeField] private AudioClip _collisionSound;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _soundDetectionRadius = 10f;

        public AudioSource AudioSource => _audioSource;
        public AudioClip CollisionSound => _collisionSound;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SoundPlay()
        {
            if (_audioSource != null && _collisionSound != null)
            {
                _audioSource.PlayOneShot(_collisionSound);
                DetectEnemies();
            }
        }

        private void DetectEnemies()
        {
            EnemyControll[] enemies = UnityEngine.Object.FindObjectsByType<EnemyControll>(UnityEngine.FindObjectsSortMode.None);

            foreach (var enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= _soundDetectionRadius)
                {
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    enemy.SetSoundSource(transform.position);
                    enemy.GetChaseState();
                }
            }
        }


    }
}
