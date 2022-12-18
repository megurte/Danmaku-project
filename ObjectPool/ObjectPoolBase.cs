using System;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Interfaces;
using UnityEngine;
using Utils;

namespace ObjectPool
{
    public class ObjectPoolBase: MonoBehaviour
    {
        [Serializable]
        public class Pool
        {
            public ObjectPoolTags tag;
            public GameObject prefab;
            public int maxSize;
        }

        public static ObjectPoolBase Instance;

        public List<Pool> pools;
        private static Dictionary<ObjectPoolTags, Queue<GameObject>> _objectPoolDictionary;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _objectPoolDictionary = new Dictionary<ObjectPoolTags, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.maxSize; i++)
                {
                    var objectInstance = Instantiate(pool.prefab);
                    
                    objectInstance.SetActive(false);
                    objectPool.Enqueue(objectInstance);
                }
                
                _objectPoolDictionary.Add(pool.tag, objectPool);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static GameObject GetBulletFromPool(ObjectPoolTags dictionaryTag, Vector3 position)
        {
            if (!_objectPoolDictionary.ContainsKey(dictionaryTag))
            {
                Debug.LogWarning("No tag in dictionary: " + dictionaryTag);
                return null;
            }
            
            var targetObject = _objectPoolDictionary[dictionaryTag].Dequeue();
            
            targetObject.SetActive(true);
            targetObject.transform.position = position;

            var pooledObject = targetObject.GetComponent<IPooledType>();
            
            pooledObject?.OnObjectPooled();

            _objectPoolDictionary[dictionaryTag].Enqueue(targetObject);
            
            return targetObject;
        }

        public static void HideObjectsInPool(ObjectPoolTags dictionaryTag = ObjectPoolTags.Default)
        {
            Queue<GameObject> queue;
            
            if (dictionaryTag == ObjectPoolTags.Default)
            {
                foreach (var tag in _objectPoolDictionary)
                {
                    queue = _objectPoolDictionary[tag.Key];

                    foreach (var item in queue.Where(item => item))
                    {
                        //item.HasComponent<Bullet>(component => component.DestroySelf());
                        item.SetActive(false);
                    }
                }
                
                return;
            }
            
            queue = _objectPoolDictionary[dictionaryTag];

            foreach (var item in queue.Where(item => item))
            {
                //item.HasComponent<Bullet>(component => component.DestroySelf());
                item.SetActive(false);
            }
        }
    }
}