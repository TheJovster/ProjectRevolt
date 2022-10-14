using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Saving 
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem savingSystem;

        const string defaultSaveFile = "save";

        void Start()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                savingSystem.Save(defaultSaveFile);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystem.Load(defaultSaveFile);
            }
        }
    }
}
