using UnityEngine;

namespace Raccons_House_Games
{
    [CreateAssetMenu(fileName = "ObjectType", menuName ="CoonGame/ObjectTypes/ObjectType")]
    public class ObjectsType : ScriptableObject
    {
        [field: SerializeField] public ItemObjects ItemObjectType { get; private set; }
        public GameObject[] ObjectsPrefab;
    }
}