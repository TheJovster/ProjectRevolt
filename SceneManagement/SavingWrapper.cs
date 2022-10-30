using ProjectRevolt.Saving;
using System.Collections;
using UnityEngine;

namespace ProjectRevolt.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField]private SavingSystem savingSystem;
        Fader fader;

        const string defaultSaveFile = "save";

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            fader = FindObjectOfType<Fader>();
            //fade out
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            //fade in
            yield return fader.FadeIn(1.5f);
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
            if (Input.GetKeyDown(KeyCode.D)) 
            {
                Delete();
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

        public void Delete() 
        {
            savingSystem.Delete(defaultSaveFile);
        }
    }
}
