using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class TrashController : MonoBehaviour
    {
        [SerializeField] private GameTrash _trashData;
        [SerializeField] private Vector2 _spawnArea = new Vector2(10f, 10f);
        [SerializeField] private int _numberOfTrashToSpawn = 10;
        private List<GameObject> _trashDictionary = new List<GameObject>();
        private ObjectPool _trashPool;

        private void Start()
        {
            _trashPool = new ObjectPool(_trashData.TrashPrefabs[0], 15, 15, transform);
            SpawnTrash();
        }
        
        private void SpawnTrash()
        {
            for(int i = 0; i < _numberOfTrashToSpawn; i++)
            {
                GameObject trash = _trashPool.Get();
                if(trash != null)
                {
                    Vector3 randomPosition = new Vector3(
                        Random.Range(-_spawnArea.x / 2f, _spawnArea.x / 2f),
                        0.5f, // height
                        Random.Range(-_spawnArea.y / 2f, _spawnArea.y / 2f)
                    );

                    trash.transform.position = transform.position + randomPosition;
                    trash.SetActive(true);
                }
            }
        }
    }
}
