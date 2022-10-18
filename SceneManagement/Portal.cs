using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using ProjectRevolt.Saving;

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

        [SerializeField] private float fadeOutTime = 2f;
        [SerializeField] private float fadeInTime = 2f;
        [SerializeField] private float fadeWaitTime = .5f;
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

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeOutTime);
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
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
