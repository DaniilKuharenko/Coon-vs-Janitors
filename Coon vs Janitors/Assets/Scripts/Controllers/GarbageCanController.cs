using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Raccons_House_Games
{
    public class GarbageCanController : MonoBehaviour
    {
        [SerializeField] private float _interactionTime = 2f; // Button hold time (in seconds)
        [SerializeField] private GameObject _exclamationImage; 
        [SerializeField] private GameObject _CrossImage; 
        private List<GameObject> _trashInCan;
        private bool _isPlayerInZone = false;
        private bool _isInteracting = false;

        public void InitializeTrash(List<GameObject> trash)
        {
            _trashInCan = trash;

            // Debugging: Check how much garbage is in the tank
            Debug.Log($"Initializing the trash can. In the trash can: {_trashInCan.Count} objects.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInZone = true;
                Debug.Log("The player has entered the zone!");

                // Check if there is trash in the can and activate corresponding image
                if (_trashInCan.Count > 0)
                {
                    _exclamationImage.SetActive(true);
                    _CrossImage.SetActive(false);
                }
                else
                {
                    _exclamationImage.SetActive(false);
                    _CrossImage.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInZone = false;
                _isInteracting = false; // Abort the interaction
                Debug.Log("The player has left the zone!");

                // Hide both images when the player leaves the zone
                _exclamationImage.SetActive(false);
                _CrossImage.SetActive(false);
            }
        }

        private void Update()
        {
            if (_isPlayerInZone && Input.GetKey(KeyCode.E)) // Test pressing the button
            {
                if (!_isInteracting)
                {
                    Debug.Log("The beginning of the interaction...");
                    StartCoroutine(StartInteraction());
                }
            }
            else if (_isInteracting && Input.GetKeyUp(KeyCode.E))
            {
                _isInteracting = false;
                Debug.Log("The interaction has been interrupted.");
            }
        }

        private IEnumerator StartInteraction()
        {
            _isInteracting = true;

            // Wait time
            Debug.Log($"Time wait {_interactionTime} seconds...");
            yield return new WaitForSeconds(_interactionTime);

            // Check if the player held down the button and if there is garbage in the tank
            if (_isInteracting && _trashInCan.Count > 0)
            {
                Debug.Log("The trash is coming out! :O");

                // Loop through all objects in the trash can and make them active
                foreach (var trash in _trashInCan)
                {
                    trash.transform.position = transform.position + Vector3.up; // Place it at a specific position
                    trash.SetActive(true); // Activate the object
                }
                
                // Clear the trash after it's been released
                _trashInCan.Clear();

                // Hide the exclamation image and show the cross image when the trash is released
                _exclamationImage.SetActive(false);
                _CrossImage.SetActive(true);
            }
            else
            {
                Debug.Log("No debris in tank or interaction interrupted.");
            }

            _isInteracting = false;
        }
    }
}
