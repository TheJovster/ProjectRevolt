using ProjectRevolt.Saving;
using System.Collections;
using UnityEngine;

namespace ProjectRevolt.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem savingSystem;
        Fader fader;

        const string defaultSaveFile = "save";

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            fader = FindObjectOfType<Fader>();
        }

        private IEnumerator Start()
        {
            //fade out
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
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
