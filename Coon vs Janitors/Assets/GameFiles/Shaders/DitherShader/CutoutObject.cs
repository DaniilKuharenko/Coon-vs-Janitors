using UnityEngine;

namespace Raccons_House_Games
{
    public class CutoutObject : MonoBehaviour
    {
        [SerializeField] private Transform _targetObject;
        [SerializeField] private LayerMask _wallMask;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            Vector2 cutoutPos = _camera.WorldToViewportPoint(_targetObject.position);
            cutoutPos.y /= Screen.width / Screen.height;

            Vector3 offset = _targetObject.position - transform.position;
            RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, _wallMask);

            for(int i = 0; i < hitObjects.Length; i++)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
                for(int m = 0; m < materials.Length; m++)
                {
                    materials[m].SetVector("_CutoutPos", cutoutPos);
                    materials[m].SetFloat("_CutoutSize", 0.1f);
                    materials[m].SetFloat("_FalloffSize", 0.05f);
                }
            }
        }
    }
}
