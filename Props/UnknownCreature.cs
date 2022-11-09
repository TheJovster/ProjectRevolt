
using ProjectRevolt.Attributes;
using Unity.VisualScripting;
using UnityEngine;

public class UnknownCreature : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject healFX;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            audioSource.PlayOneShot(soundFX[Random.Range(0, soundFX.Length)]);
            other.gameObject.GetComponent<Health>().Heal(25f);
            GameObject healFXInstance = Instantiate(healFX, other.transform.position + Vector3.up, Quaternion.identity);
            Destroy(healFXInstance, .75f);
            leftEye.SetActive(false);
            rightEye.SetActive(false);
            GetComponent<Collider>().enabled = false;
            
        }
    }
}
