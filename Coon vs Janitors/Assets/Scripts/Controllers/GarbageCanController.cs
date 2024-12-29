using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class GarbageCanController : MonoBehaviour
    {
        [SerializeField] private float _interactionTime = 4f; // Button hold time (in seconds)
        [SerializeField] private GameObject _exclamationImage;
        [SerializeField] private GameObject _CrossImage;
        [SerializeField] private GameObject _buttonUI;
        [SerializeField] private GameObject _loadingImage;
        [SerializeField] private float _throwUpImpulse = 5f;
        [SerializeField] private float _throwRadiusImpulse = 3f;
        [SerializeField] private Button _interactionButton;

        private List<GameObject> _trashInCan;
        private bool _isPlayerInZone = false;
        private bool _isInteracting = false;

        public void InitializeTrash(List<GameObject> trash)
        {
            _trashInCan = trash;

            if (_interactionButton != null)
            {
                _interactionButton.onClick.AddListener(OnInteractionButtonClicked);
            }

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
                    _exclamationImage.SetActive(true); // Show the exclamation image
                    _buttonUI.SetActive(true);
                    _CrossImage.SetActive(false); // Hide the cross image
                }
                else
                {
                    _buttonUI.SetActive(false);
                    _exclamationImage.SetActive(false); // Hide the exclamation image
                    _CrossImage.SetActive(true); // Show the cross image
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
                _buttonUI.SetActive(false);
            }
        }

        private void OnInteractionButtonClicked()
        {
            if (!_isInteracting && _isPlayerInZone)
            {
                Debug.Log("The beginning of the interaction...");
                StartCoroutine(StartInteraction());
            }
        }

        private IEnumerator StartInteraction()
        {
            _isInteracting = true;

            // Start rotating the loading image
            float rotationSpeed = 360f / _interactionTime; // 360 degrees in the interaction time
            float elapsedTime = 0f;

            // Activate loading image and start rotating it
            _loadingImage.SetActive(true);

            while (elapsedTime < _interactionTime)
            {
                _loadingImage.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Stop rotation and reset angle after interaction time is over
            _loadingImage.SetActive(false);
            _loadingImage.transform.rotation = Quaternion.identity;

            // Check if the player held down the button and if there is garbage in the tank
            if (_trashInCan.Count > 0)
            {
                Debug.Log("The trash is coming out sequentially!");

                foreach (var trash in _trashInCan)
                {
                    trash.transform.position = transform.position + Vector3.up; // Place it at a specific position
                    trash.SetActive(true); // Activate the object

                    // Apply vertical impulse (upward force)
                    Rigidbody rb = trash.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // Give upward impulse
                        rb.velocity = Vector3.zero; // Reset velocity to ensure consistent behavior
                        rb.AddForce(Vector3.up * _throwUpImpulse, ForceMode.Impulse);

                        // Apply a random horizontal impulse (around the can) with radius control
                        Vector2 randomDirection = Random.insideUnitCircle.normalized * _throwRadiusImpulse;
                        rb.AddForce(new Vector3(randomDirection.x, 0, randomDirection.y), ForceMode.Impulse);
                    }

                    // Pause briefly before the next piece of trash is thrown
                    yield return new WaitForSeconds(0.3f); // Adjust the delay for desired timing
                }

                // Clear the trash after it's been released
                _trashInCan.Clear();

                // Hide the exclamation image and show the cross image when the trash is released
                _exclamationImage.SetActive(false);
                _buttonUI.SetActive(false);
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
