using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class GarbageCanController : MonoBehaviour
    {
        [SerializeField] private float _interactionTime = 2f; // Button hold time (in seconds)
        private List<GameObject> _trashInCan;
        private TrashController _trashController;
        private bool _isPlayerInZone = false;
        private bool _isInteracting = false;

        public void InitializeTrash(List<GameObject> trash, TrashController trashController)
        {
            _trashInCan = trash;
            _trashController = trashController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInZone = false;
                _isInteracting = false; // Abort the interaction
            }
        }

        private void Update()
        {
            if (_isPlayerInZone && Input.GetKey(KeyCode.E)) // Test button
            {
                if (!_isInteracting)
                {
                    StartCoroutine(StartInteraction());
                }
            }
            else if (_isInteracting && Input.GetKeyUp(KeyCode.E))
            {
                _isInteracting = false; // Interrupt the interaction if the button is released
            }
        }

        private IEnumerator StartInteraction()
        {
            _isInteracting = true;

            // wait time to end
            yield return new WaitForSeconds(_interactionTime);

            // Check if the player held the button all the time
            if (_isInteracting && _trashInCan.Count > 0)
            {
                // Let the garbage out to walking
                GameObject trash = _trashInCan[0];
                _trashInCan.RemoveAt(0);
                trash.transform.position = transform.position + Vector3.up;
                trash.SetActive(true);
            }

            _isInteracting = false;
        }
    }
}
