using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;
    [HideInInspector]public bool isAlive = true;

    //animation
    private Animator animator;

    //Audio
    [Header("Audio")]
    [SerializeField] private AudioClip[] takeDamageClips;
    [SerializeField] private AudioClip deathSFX;
    private AudioSource audioSource;

    //visual FX
    [Header("VFX")]
    [SerializeField] private ParticleSystem bloodFX;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageToTake) 
    {
        if (isAlive)
        {

            bloodFX.Play();
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
            {
                currentHealth = 0f;
                animator.SetTrigger("Die"); //might want to substitute this with a ragdoll?
                audioSource.PlayOneShot(deathSFX);
                isAlive = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
            else 
            {
                int takeDamageSFXIndex = Random.Range(0, takeDamageClips.Length);
                audioSource.PlayOneShot(takeDamageClips[takeDamageSFXIndex]);
                animator.SetTrigger("TakeDamage");
            }
        }
        else 
        {
            return;
        }
    }
}
