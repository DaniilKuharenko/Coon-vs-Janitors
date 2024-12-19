using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class ObjectPool
    {
        [SerializeField] private int _maxCount;
        private Queue<GameObject> _pool = new Queue<GameObject>();
        private GameObject _prefab;
        private Transform _parent;

        //Testing varible
        private int _totalCreated;

        public ObjectPool(GameObject prefab, int initialCount, int maxCount, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _maxCount = maxCount;


            for(int i = 0; i < initialCount; i++)
            {
                GameObject PoolObject = Object.Instantiate(_prefab, _parent);
                Debug.Log("Test Instantiate 1");
                PoolObject.SetActive(true);
                _pool.Enqueue(PoolObject);
            }
            Debug.Log($"Pool initialized with {initialCount} objects.");
        }

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                GameObject poolObject = _pool.Dequeue();
                poolObject.SetActive(true);
                Debug.Log($"Object {poolObject.name} fetched from pool. Pool size: {_pool.Count}");
                return poolObject;
            }

            if (_totalCreated < _maxCount)
            {
                _totalCreated++;
                GameObject poolObject = Object.Instantiate(_prefab, _parent);
                poolObject.SetActive(true);
                Debug.Log($"Pool is empty! Creating new object. Total created: {_totalCreated}");
                return poolObject;
            }
            
            // If the limit is reached, reuse the existing object
            GameObject reusedObject = _pool.Dequeue();
            reusedObject.SetActive(true);
            Debug.LogWarning($"Max pool size reached! Reusing object: {reusedObject.name}");
            return reusedObject;
        }

        public void Return(GameObject poolObject)
        {
            poolObject.SetActive(false);
            if (_pool.Count < _maxCount)
            {
                _pool.Enqueue(poolObject);
                Debug.Log($"Object returned to pool. Pool size: {_pool.Count}");
            }
            else
            {
                Debug.LogWarning($"Pool size exceeded max count! Destroying object: {poolObject.name}");
                Object.Destroy(poolObject);
            }
        }


    }
}
