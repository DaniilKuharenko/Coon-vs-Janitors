using UnityEngine;
using UnityEngine.UIElements;

namespace Raccons_House_Games
{
    public class StoneCollision : Items
    {
        private bool _hasPlayedSound = false;
        private Rigidbody _rigidbody;
        [SerializeField] private float _minImpactVelocity = 2.0f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") && !_hasPlayedSound)
            {
                if (_rigidbody != null && _rigidbody.velocity.magnitude >= _minImpactVelocity)
                {
                    gameObject.tag = "Stone";
                    
                    EnemyControll[] enemies = UnityEngine.Object.FindObjectsByType<EnemyControll>(UnityEngine.FindObjectsSortMode.None);

                    foreach (var enemy in enemies)
                    {
                        enemy.CheckForFallenObject();
                    }
                    SoundPlay();
                    _hasPlayedSound = true;
                    ResetState();
                }
            }
        }

        public void ResetState()
        {
            _hasPlayedSound = false;
            gameObject.tag = "Pickup";
        }
    }
}
