using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class TrashController : MonoBehaviour
    {
        [SerializeField] private GameObject _garbageCanPrefab;
        [SerializeField] private GameTrash _trashData;
        [SerializeField] private Vector2 _spawnArea = new Vector2(10f, 10f);
        [SerializeField] private int _numberOfTrashToSpawn = 10; // Total amount of garbage
        [SerializeField] private int _numberOfGarbageCans = 5;
        [SerializeField] private int _trashPerCan = 5; // Trash to the tank

        private readonly List<GameObject> _activeTrash = new List<GameObject>();
        private readonly List<GameObject> _activeGarbageCans = new List<GameObject>();
        private readonly List<ObjectPool> _trashPools = new List<ObjectPool>();
        private ObjectPool _garbageCanPool;

        private void Start()
        {
            // Initialization of trash pools
            foreach (var prefab in _trashData.TrashPrefabs)
            {
                var pool = new ObjectPool(prefab, transform, 65);
                _trashPools.Add(pool);
            }

            // Initialization of the garbage pool
            _garbageCanPool = new ObjectPool(_garbageCanPrefab, transform, _numberOfGarbageCans);

            SpawnTrash();
            SpawnGarbageCans();
        }

        private void SpawnTrash()
        {
            // Half of the garbage is spawned as separate objects
            int trashToSpawnIndividually = _numberOfTrashToSpawn / 2;

            for (int i = 0; i < trashToSpawnIndividually; i++)
            {
                SpawnTrashAtRandomPosition();
            }
        }

        private void SpawnGarbageCans()
        {
            int remainingTrash = _numberOfTrashToSpawn / 2;
            int trashAssigned = 0;

            for (int i = 0; i < _numberOfGarbageCans; i++)
            {
                GameObject garbageCan = _garbageCanPool.GetFromPool();
                if (garbageCan != null)
                {
                    Vector3 randomPosition = new Vector3(
                        Random.Range(-_spawnArea.x / 2f, _spawnArea.x / 2f),
                        0.5f, // height
                        Random.Range(-_spawnArea.y / 2f, _spawnArea.y / 2f)
                    );

                    garbageCan.transform.position = transform.position + randomPosition;
                    garbageCan.SetActive(true);

                    _activeGarbageCans.Add(garbageCan); // Add the tank to the list of active tanks

                    // Distribution of trash into the tank
                    var canController = garbageCan.GetComponent<GarbageCanController>();
                    if (canController != null)
                    {
                        List<GameObject> trashForCan = new List<GameObject>();

                        for (int j = 0; j < _trashPerCan && trashAssigned < remainingTrash; j++)
                        {
                            GameObject trash = SpawnTrashAtRandomPosition(false); // Получаем мусор
                            if (trash != null)
                            {
                                trashForCan.Add(trash);
                                trashAssigned++;
                            }
                        }

                        // Transferring the trash to the tank
                        canController.InitializeTrash(trashForCan, this);
                    }
                }
            }
        }

        private GameObject SpawnTrashAtRandomPosition(bool activate = true)
        {
            int randomPoolIndex = Random.Range(0, _trashPools.Count);
            ObjectPool selectedPool = _trashPools[randomPoolIndex];

            GameObject trash = selectedPool.GetFromPool();
            if (trash != null)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(-_spawnArea.x / 2f, _spawnArea.x / 2f),
                    0.5f, // height
                    Random.Range(-_spawnArea.y / 2f, _spawnArea.y / 2f)
                );

                trash.transform.position = transform.position + randomPosition;
                trash.SetActive(activate);
                if (activate)
                {
                    _activeTrash.Add(trash);
                }
            }

            return trash;
        }

        public void ClearTrash()
        {
            // Deactivating the trash
            foreach (var trash in _activeTrash)
            {
                trash.SetActive(false);
                foreach (var pool in _trashPools)
                {
                    pool.ReturnToPool(trash);
                }
            }
            _activeTrash.Clear();

            // Deactivating the trash tank
            foreach (var garbageCan in _activeGarbageCans)
            {
                garbageCan.SetActive(false);
                _garbageCanPool.ReturnToPool(garbageCan);
            }
            _activeGarbageCans.Clear();
        }

        public void ReturnTrashToPool(GameObject trash)
        {
            if (trash != null)
            {
                trash.SetActive(false);
                foreach (var pool in _trashPools)
                {
                    if (pool.IsPartOfPool(trash))
                    {
                        pool.ReturnToPool(trash);
                        break;
                    }
                }
            }
        }
    }
}
