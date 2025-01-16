using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TrashController _trashControl;
        [SerializeField] private ReferenceHolder _referenceHolder;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private ObjectsType[] _objectTypes;
        [SerializeField] private Image _uiImage;
        private GpuInctancingEnabler _gpuInctancingEnabler;


        private ObjectPool _playerPool;
        private List<ObjectPool> _enemyPools;
        private Dictionary<ItemObjects, ObjectPool> _itemObjectsPool;
        private GameObject _playerInstance;

        public void StartGame()
        {
            InitializePools();
            SpawnObjects();
            _trashControl.TrashInitialize();
            SpawnPlayer();
            SpawnEnemies();
        }

        private void InitializePools()
        {
            //Create a pool for the Objects
            _itemObjectsPool = new Dictionary<ItemObjects, ObjectPool>();
            foreach (var objectType in _objectTypes)
            {
                if(objectType.ObjectsPrefab.Length > 0 && !_itemObjectsPool.ContainsKey(objectType.ItemObjectType))
                {
                    var pool = new ObjectPool(objectType.ObjectsPrefab[0], _poolParent, 50);
                    _itemObjectsPool.Add(objectType.ItemObjectType, pool);
                }
            } 

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
            ItemObjects randomType = (ItemObjects)Random.Range(0, System.Enum.GetValues(typeof(ItemObjects)).Length);

            if(_itemObjectsPool.TryGetValue(randomType, out var pool))
            {
                int objectCount = Random.Range(10, 51);

                for(int i = 0; i < objectCount; i++)
                {
                    var objectInstance = pool.GetFromPool();
                    objectInstance.transform.position = RandomPosition();
                    objectInstance.SetActive(true);
                }
                
                ObjectsType selectedType = System.Array.Find(_objectTypes, t => t.ItemObjectType == randomType);
                if (selectedType != null && selectedType.ItemObjectImage != null)
                {
                    _uiImage.sprite = selectedType.ItemObjectImage;
                }
            }
        }

        private Vector3 RandomPosition()
        {
            return new Vector3(Random.Range(-30f, 30f), 1, Random.Range(-30f, 30f));
        }
    }
}