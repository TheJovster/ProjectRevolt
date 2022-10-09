using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //animation
    private Animator animator;

    //Audio
    [SerializeField] private AudioClip[] takeDamageClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage() 
    {
        int takeDamageSFXIndex = Random.Range(0, takeDamageClips.Length);
        audioSource.PlayOneShot(takeDamageClips[takeDamageSFXIndex]);
        animator.SetTrigger("TakeDamage");
    }
}
