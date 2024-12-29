using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); 

        private readonly HashSet<Transform> _occupiedPoints = new HashSet<Transform>(); 
        private readonly List<GameObject> _activeTrash = new List<GameObject>(); 
        private readonly List<GameObject> _activeGarbageCans = new List<GameObject>(); 
        private readonly List<ObjectPool> _trashPools = new List<ObjectPool>(); 
        private ObjectPool _garbageCanPool;

        private void Start()
        {
            // Initialize trash object pools
            foreach (var prefab in _trashData.TrashPrefabs)
            {
                var pool = new ObjectPool(prefab, transform, 65);
                _trashPools.Add(pool);
            }

            // Initialize the garbage can pool
            _garbageCanPool = new ObjectPool(_garbageCanPrefab, transform, _numberOfGarbageCans);

            SpawnTrash();
            SpawnGarbageCans();
        }

        private void SpawnTrash()
        {
            // Spawn half the trash as individual objects
            int trashToSpawnIndividually = _numberOfTrashToSpawn / 2;

            for (int i = 0; i < trashToSpawnIndividually; i++)
            {
                SpawnTrashAtRandomPosition();
            }
        }

        private void SpawnGarbageCans()
        {
            int trashAssigned = 0; // Amount of trash assigned

            for (int i = 0; i < _numberOfGarbageCans; i++)
            {
                // Find a random available spawn point
                Transform spawnPoint = GetRandomAvailablePoint();
                if (spawnPoint == null)
                {
                    Debug.LogWarning("No available spawn points for garbage cans!");
                    break;
                }

                GameObject garbageCan = _garbageCanPool.GetFromPool();
                if (garbageCan != null)
                {
                    garbageCan.transform.position = spawnPoint.position;
                    garbageCan.SetActive(true);
                    _occupiedPoints.Add(spawnPoint); // Mark the spawn point as occupied
                    _activeGarbageCans.Add(garbageCan);

                    var canController = garbageCan.GetComponent<GarbageCanController>();
                    if (canController != null)
                    {
                        List<GameObject> trashForCan = new List<GameObject>();

                        // Assign trash to this garbage can
                        for (int j = 0; j < _trashPerCan; j++)
                        {
                            GameObject trash = SpawnTrashAtRandomPosition(false);
                            if (trash != null)
                            {
                                trashForCan.Add(trash);
                                trashAssigned++;
                            }
                        }

                        Debug.Log($"Garbage can {i} received {trashForCan.Count} trash objects.");
                        canController.InitializeTrash(trashForCan);
                    }
                }
            }

            if (trashAssigned < _numberOfTrashToSpawn)
            {
                Debug.LogWarning($"Not all trash could be assigned! Assigned: {trashAssigned}, Total: {_numberOfTrashToSpawn}");
            }
        }

        private Transform GetRandomAvailablePoint()
        {
            // Filter out occupied points
            var availablePoints = spawnPoints.Where(p => !_occupiedPoints.Contains(p)).ToList();

            if (availablePoints.Count == 0)
            {
                return null; // No available points
            }

            // Pick a random free point
            return availablePoints[Random.Range(0, availablePoints.Count)];
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
            // Deactivate and return all trash to pools
            foreach (var trash in _activeTrash)
            {
                trash.SetActive(false);
                foreach (var pool in _trashPools)
                {
                    pool.ReturnToPool(trash);
                }
            }
            _activeTrash.Clear();

            // Deactivate and return all garbage cans to the pool
            foreach (var garbageCan in _activeGarbageCans)
            {
                garbageCan.SetActive(false);
                _garbageCanPool.ReturnToPool(garbageCan);
            }
            _activeGarbageCans.Clear();
            _occupiedPoints.Clear(); // Clear the occupied points list
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
