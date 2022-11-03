using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownCreature : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundFX;
    [SerializeField] private AudioSource audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            audioSource.PlayOneShot(soundFX[Random.Range(0, soundFX.Length)]);
        }
    }
}
