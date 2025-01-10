using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ReferenceHolder _referenceHolder;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private Transform _poolParent;

        private ObjectPool _playerPool;
        private List<ObjectPool> _enemyPools;
        private GameObject _playerInstance;

        public void Start()
        {
            InitializePools();
            SpawnPlayer();
            SpawnEnemies();
        }

        private void InitializePools()
        {
            //Create a pool for the Objects
            

            // Create a pool for the player
           //_playerPool = new ObjectPool(_referenceHolder.PlayerPrefab.gameObject, _poolParent, 1);

            // Create pools for the enemies
            _enemyPools = new List<ObjectPool>();
            foreach(var enemyPrefab in _referenceHolder.EnemyPrefabs)
            {
                var pool = new ObjectPool(enemyPrefab.gameObject, _poolParent, 3);
                _enemyPools.Add(pool);
            }
        }

        private void SpawnPlayer()
        {
            // Get a player from the pool
            // _playerInstance = _playerPool.GetFromPool();
            // _playerInstance.transform.position = _playerSpawnPoint.position;

            // Initialize the player's state machine
            // var playerControll = _playerInstance.GetComponent<PlayerControll>();
            // playerControl.InitializeStateMachine();
        }

        private void SpawnEnemies()
        {
            // For each spawn point, select a random pool of enemies and spawn them
            foreach(var spawnPoint in _enemySpawnPoints)
            {
                var pool = _enemyPools[Random.Range(0, _enemyPools.Count)];
                var enemyInstance = pool.GetFromPool();
                enemyInstance.transform.position = spawnPoint.position;

                // Initialize the enemys's state machine
                var enemyControll = enemyInstance.GetComponent<EnemyControll>();
                enemyControll.InitializeStateMachine();
                enemyControll.InitializeEnemyControll();
            }
        }

        private void SpawnObjects()
        {

        }
    }
}