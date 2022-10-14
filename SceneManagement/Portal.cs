using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace ProjectRevolt.SceneManagement 
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier 
        {
            A,
            B,
            C,
            D
        }

        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] private int sceneToLoad = -1;
        public Transform spawnPoint;

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
                }//ignore this - it's just for debugging
                
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition() 
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal() 
        {
            foreach(Portal portal in FindObjectsOfType<Portal>()) 
            {
                if (portal == this)
                {
                    continue;
                }
                if (portal.destination != destination) continue;

                else return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal) 
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
