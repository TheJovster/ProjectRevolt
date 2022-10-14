using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectRevolt.SceneManagement 
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player") 
            {
                if(sceneToLoad == 1) 
                {
                    Debug.Log("You set out to the Goblin Forest");
                }
                else if(sceneToLoad == 0) 
                {
                    Debug.Log("You set out to the Burned Village");
                }
                
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition() 
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Debug.Log("Scene Loaded");
            Destroy(gameObject);
        }
    }
}
