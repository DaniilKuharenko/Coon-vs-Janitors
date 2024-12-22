using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class ObjectPool
    {
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public ObjectPool(GameObject prefab, Transform parent, int initialSize)
        {
            _prefab = prefab;
            _parent = parent;

            Debug.Log($"Initializing ObjectPool with prefab: {_prefab.name}, initial size: {initialSize}");

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = CreateObject();
                ReturnToPool(obj);
            }

            Debug.Log($"ObjectPool initialized with {_pool.Count} objects.");
        }

        private GameObject CreateObject()
        {
            GameObject obj = Object.Instantiate(_prefab, _parent);
            obj.SetActive(false);
            Debug.Log($"Created new object: {obj.name}");
            return obj;
        }

        public GameObject GetFromPool()
        {
            if (_pool.Count > 0)
            {
                GameObject obj = _pool.Dequeue();
                obj.SetActive(true);
                Debug.Log($"Object {obj.name} fetched from pool. Remaining objects in pool: {_pool.Count}");
                return obj;
            }

            Debug.LogWarning("Pool is empty. Creating new object.");
            return CreateObject();
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
            Debug.Log($"Object {obj.name} returned to pool. Total objects in pool: {_pool.Count}");
        }
    }
}
