using UnityEngine;

namespace Raccons_House_Games
{
    public class ItemPickup : MonoBehaviour
    {
        public Transform holdPoint;
        private GameObject pickedUpItem;

        private void Update()
        {
            if (pickedUpItem != null && Input.GetKeyDown(KeyCode.E))
            {
                DropItem();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup") && pickedUpItem == null)
            {
                pickedUpItem = other.gameObject;
                PickUpItem();
            }
        }

        private void PickUpItem()
        {

            Rigidbody rb = pickedUpItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            pickedUpItem.transform.SetParent(holdPoint);
            pickedUpItem.transform.localPosition = Vector3.zero;
        }


        private void DropItem()
        {
            Debug.Log("DropItem() is called");
            if (pickedUpItem != null)
            {
                Rigidbody rb = pickedUpItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddForce(transform.forward * 20.0f, ForceMode.Impulse);
                }

                pickedUpItem.transform.SetParent(null);
                pickedUpItem = null;

                Debug.Log("Subject dropped.");
            }
            else
            {
                Debug.Log("There is no item to reset.");
            }
        }


    }
}
