using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class PointerManager : MonoBehaviour 
    {

        [SerializeField] PointerIcon _pointerPrefab;
        private Dictionary<ObjectPointer, PointerIcon> _dictionary = new Dictionary<ObjectPointer, PointerIcon>();
        [SerializeField] Transform _playerTransform;
        [SerializeField] Camera _camera;

        public static PointerManager Instance;
        public void InitializePointerManager() 
        {
            _pointerPrefab.InitializePointerIcon();
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        public void AddToList(ObjectPointer objectPointer) {
            PointerIcon newPointer = Instantiate(_pointerPrefab, transform);
            _dictionary.Add(objectPointer, newPointer);
        }

        public void RemoveFromList(ObjectPointer objectPointer) {
            Destroy(_dictionary[objectPointer].gameObject);
            _dictionary.Remove(objectPointer);
        }

        void LateUpdate() {

            // Left, Right, Down, Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var kvp in _dictionary) {

                ObjectPointer objectPointer = kvp.Key;
                PointerIcon pointerIcon = kvp.Value;

                Vector3 toEnemy = objectPointer.transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, toEnemy);
                Debug.DrawRay(_playerTransform.position, toEnemy);


                float rayMinDistance = Mathf.Infinity;
                int index = 0;

                for (int p = 0; p < 4; p++) {
                    if (planes[p].Raycast(ray, out float distance)) {
                        if (distance < rayMinDistance) {
                            rayMinDistance = distance;
                            index = p;
                        }
                    }
                }

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 position = _camera.WorldToScreenPoint(worldPosition);
                Quaternion rotation = GetIconRotation(index);

                if (toEnemy.magnitude > rayMinDistance) {
                    pointerIcon.Show();
                } else {
                    pointerIcon.Hide();
                }

                pointerIcon.SetIconPosition(position, rotation);
            }

        }

        Quaternion GetIconRotation(int planeIndex) {
            if (planeIndex == 0) {
                return Quaternion.Euler(0f, 0f, 90f);
            } else if (planeIndex == 1) {
                return Quaternion.Euler(0f, 0f, -90f);
            } else if (planeIndex == 2) {
                return Quaternion.Euler(0f, 0f, 180);
            } else if (planeIndex == 3) {
                return Quaternion.Euler(0f, 0f, 0f);
            }
            return Quaternion.identity;
        }

    }
}
