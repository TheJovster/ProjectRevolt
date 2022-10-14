using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Saving 
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile) 
        {
            Debug.Log("Saving to " + saveFile);
        }

        public void Load(string saveFile) 
        {
            Debug.Log("Loading from " + saveFile);
        }
    }
}
