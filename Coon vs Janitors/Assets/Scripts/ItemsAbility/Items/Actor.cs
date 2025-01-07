using UnityEngine;

namespace Items
{
    public class Actor : MonoBehaviour
    {
        [field: SerializeField] public Transform SelfTansform { get; private set; }
        //[field: SerializeField] public NavMeshMovement Movement { get; private set; }
        //[field: SerializeField] public CHealth Health { get; private set; }

    }
}