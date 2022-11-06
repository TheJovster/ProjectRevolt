using ProjectRevolt.Saving;
using System.Collections;
using UnityEngine;

namespace ProjectRevolt.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode saveKey = KeyCode.S;
        [SerializeField] KeyCode loadKey = KeyCode.L;
        [SerializeField] KeyCode deleteKey = KeyCode.Delete;

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
            if (Input.GetKeyDown(saveKey))
            {
                Save();
            }
            if (Input.GetKeyDown(loadKey))
            {
                Load();
            }
            if (Input.GetKeyDown(deleteKey)) 
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
            StartCoroutine(savingSystem.LoadLastScene(defaultSaveFile));
        }

        public void Delete() 
        {
            savingSystem.Delete(defaultSaveFile);
        }
    }
}
