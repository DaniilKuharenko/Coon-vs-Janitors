using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private AudioClip _collisionSound;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void AlertEnemys()
        {
            
        }


    }
}
