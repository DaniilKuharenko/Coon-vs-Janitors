using UnityEngine;
using UnityEngine.AI;
using Utilites.Health;

namespace Items
{
    public class Actor : MonoBehaviour
    {
        [field: SerializeField] public Transform SelfTansform { get; private set; }
        [field: SerializeField] public NavMeshAgent Movement { get; private set; }
        [field: SerializeField] public CHealth Health { get; private set; }

    }
}