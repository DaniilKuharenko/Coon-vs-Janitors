using UnityEngine;

namespace Raccons_House_Games
{
    public class Items : MonoBehaviour
    {   
        [SerializeField] private AudioClip _collisionSound;
        [SerializeField] private AudioSource _audioSource;

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
            }
        }

    }
}
