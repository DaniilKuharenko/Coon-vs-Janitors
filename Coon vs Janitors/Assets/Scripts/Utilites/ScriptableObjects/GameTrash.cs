using UnityEngine;

namespace Raccons_House_Games
{
    [CreateAssetMenu(fileName = "NewTrashData", menuName = "CoonGame/Trash")]
    public class GameTrash : ScriptableObject
    {
        public GameObject[] TrashPrefabs;
    }
}
