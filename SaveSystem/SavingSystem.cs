using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace ProjectRevolt.Saving 
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile) 
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) 
            {
                byte[] bytes = Encoding.UTF8.GetBytes("This is a text message."); //serializes and encodes data
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void Load(string saveFile) 
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);
            using(FileStream stream = File.Open(path, FileMode.Open)) 
            {
                byte[] byteBuffer = new byte[stream.Length];
                stream.Read(byteBuffer, 0, byteBuffer.Length); //de-serializes and decodes data (reads it)
                Debug.Log(Encoding.UTF8.GetString(byteBuffer));
            }
        }

        private string GetPathFromSaveFile(string saveFile) 
        {
            
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
