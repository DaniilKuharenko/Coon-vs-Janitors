using UnityEngine;

namespace Raccons_House_Games
{
    [CreateAssetMenu(fileName = "ReferenceHolder", menuName = "CoonGame/ReferenceHolder")]
    public class ReferenceHolder : ScriptableObject
    {
        public PlayerControll PlayerPrefab;
        public EnemyControll[] EnemyPrefabs;
    }
}
