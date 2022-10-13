using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectRevolt.SceneManagement 
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player") 
            {
                Debug.Log("You set out to the Goblin Forest");
                SceneManager.LoadSceneAsync(sceneToLoad);
            }
        }
    }
}
