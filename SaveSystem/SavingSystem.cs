using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Resources;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ProjectRevolt.Saving 
{
    public class SavingSystem : MonoBehaviour
    {

        public IEnumerator LoadLastScene(string saveFile) 
        {
            //get the state
            Dictionary<string, object> state = LoadFile(saveFile);
            //load last scene
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                int buildIndex = (int)state["lastSceneBuildIndex"];
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            //restore state
            RestoreState(state);
        }

        public void Save(string saveFile) //serializes and encodes data
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile) //de-serializes and decodes data (reads it)
        {
            RestoreState(LoadFile(saveFile));
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log(path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path)) 
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }

        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) 
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) 
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id)) 
                {
                    saveable.RestoreState(stateDictionary[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile) 
        {
            
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
