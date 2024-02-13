using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMN.PoolManager
{
    [System.Serializable]
    public class ObjectPool
    {
        // Gameobject to pool
        public GameObject prefab;

        // Maximum instances of the gameobject
        public int maximumInstances;

        // Name of the pool
        public Pools.Types poolType;
        
        [HideInInspector]
        public Dictionary<int, GameObject> passiveObjectsDictionary;

        [HideInInspector]
        public GameObject pool;

        /// <summary>
        /// Initialize the pool with creating instances of the gameobject and a container
        /// for the hieararchy
        /// </summary>
        public void InitializePool()
        {
            //activeList = new List<GameObject>();
            passiveObjectsDictionary = new Dictionary<int, GameObject>();
            pool = new GameObject("[" + poolType + "]");
            if (PoolManager.Instance.dontDestroyOnLoad)
                Object.DontDestroyOnLoad(pool);

            // Reference to the created instance
            GameObject clone;

            for (int i = 0; i < maximumInstances; i++)
            {
                // Create the gameobject
                clone = GameObject.Instantiate(prefab);

                // Deactivate and add to the container and list
                clone.SetActive(false);
                clone.transform.SetParent(pool.transform);

                passiveObjectsDictionary.Add(clone.GetInstanceID(), clone);
            }
        }

        /// <summary>
        /// Get the next gameobject that can be spawned from the pool
        /// </summary>
        /// <returns>Next gameobject to spawn</returns>
        GameObject tempObject;
        public GameObject GetNextObject()
        {
            if (passiveObjectsDictionary.Count > 0)
            {
                tempObject = passiveObjectsDictionary.Values.ElementAt(0);
                passiveObjectsDictionary.Remove(passiveObjectsDictionary.Keys.ElementAt(0));
                return tempObject;
            }
            else
            {
                Debug.Log(string.Format("PoolManager: {0} - passiveObjectsDictionary is empty. Instantiating new one.", PoolType));
                GameObject clone; 
                clone = GameObject.Instantiate(prefab);
                clone.SetActive(false);
                clone.transform.SetParent(pool.transform);
                passiveObjectsDictionary.Add(clone.GetInstanceID(), clone);

                tempObject = passiveObjectsDictionary.Values.ElementAt(0);
                passiveObjectsDictionary.Remove(passiveObjectsDictionary.Keys.ElementAt(0));
                return tempObject;
            }
        }

        // Properties
        public int MaximumInstances { get { return maximumInstances; } }
        public Pools.Types PoolType { get { return poolType; } set { poolType = value; } }
    }
}