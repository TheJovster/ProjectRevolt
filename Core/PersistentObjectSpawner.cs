using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Core 
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned = false;


        private void Start()
        {
            if(hasSpawned) 
            {
                return;
            }
            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject() 
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
