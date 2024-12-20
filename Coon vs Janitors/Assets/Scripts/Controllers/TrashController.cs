using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class TrashController : MonoBehaviour
    {
        [SerializeField] private GameTrash _trashData;
        [SerializeField] private Vector2 _spawnArea = new Vector2(10f, 10f);
        [SerializeField] private int _numberOfTrashToSpawn = 5;
        private List<GameObject> _trashDictionary = new List<GameObject>();
        private List<ObjectPool> _trashPools = new List<ObjectPool>();

        private void Start()
        {
            
            foreach (var prefab in _trashData.TrashPrefabs)
            {
                ObjectPool pool = new ObjectPool(prefab, 5, 5, transform);
                _trashPools.Add(pool);
            }

            SpawnTrash();
        }
        
        private void SpawnTrash()
        {
            for(int i = 0; i < _numberOfTrashToSpawn; i++)
            {
                int randomPoolIndex = Random.Range(0, _trashPools.Count);
                ObjectPool selectedPool = _trashPools[randomPoolIndex];

                GameObject trash = selectedPool.Get();
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
