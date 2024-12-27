using UnityEngine;

namespace Raccons_House_Games
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private Transform _holdPoint;
        private GameObject _pickedUpItem;
        private StoneCollision _stone;

        private void Start()
        {
            _stone = GetComponent<StoneCollision>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup") && _pickedUpItem == null)
            {
                _pickedUpItem = other.gameObject;
                PickUpItem();
            }
        }

        private void PickUpItem()
        {

            Rigidbody rb = _pickedUpItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            _pickedUpItem.transform.SetParent(_holdPoint);
            _pickedUpItem.transform.localPosition = Vector3.zero;
        }


        public void DropItem()
        {
            Debug.Log("DropItem() is called");
            if (_pickedUpItem != null)
            {
                if(_stone != null)
                {
                    _stone.ResetState();
                }
                Rigidbody rb = _pickedUpItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddForce(transform.forward * 10.0f, ForceMode.Impulse);
                }

                _pickedUpItem.transform.SetParent(null);
                _pickedUpItem = null;

                Debug.Log("Subject dropped.");
            }
            else
            {
                Debug.Log("There is no item to reset.");
            }
        }


    }
}
