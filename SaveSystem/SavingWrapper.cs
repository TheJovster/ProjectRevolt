using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Saving 
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem savingSystem;

        const string defaultSaveFile = "save";

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private IEnumerator Start()
        {
            yield return savingSystem.LoadLastScene(defaultSaveFile);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }
    }
}
