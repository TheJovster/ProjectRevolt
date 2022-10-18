using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace ProjectRevolt.Saving 
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile) //serializes and encodes data
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }



        public void Load(string saveFile) //de-serializes and decodes data (reads it)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) 
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
        }
        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) 
            {
                saveable.RestoreState(stateDictionary[saveable.GetUniqueIdentifier()]);
            }
        }

        private string GetPathFromSaveFile(string saveFile) 
        {
            
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
