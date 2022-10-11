using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectRevolt.Core 
{
    public class Health : MonoBehaviour, IAction
    {
        [Header("Health Variables")]
        [SerializeField] private float maxHealth = 50f;
        private float currentHealth;
        private bool isDead = false;

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

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damageToTake)
        {
            if (!IsDead())
            {
                bloodFX.Play();
                currentHealth -= damageToTake;
                if (currentHealth <= 0)
                {
                    Die();
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

        private void Die()
        {
            currentHealth = 0f;
            animator.SetTrigger("Die"); //might want to substitute this with a ragdoll?
            audioSource.PlayOneShot(deathSFX);
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void Cancel()
        {
            
        }
    }
}

