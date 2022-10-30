using ProjectRevolt.Saving;
using UnityEngine;
using ProjectRevolt.Stats;
using ProjectRevolt.Core;
using System;

namespace ProjectRevolt.Attributes 
{
    public class Health : MonoBehaviour, IAction, ISaveable
    {
        [Header("Health Variables")]
        [SerializeField]private float maxHealth = 50f;
        [SerializeField]private float currentHealth;
        private bool isDead = false;

        //delegates and events

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

        private void Start()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            currentHealth = maxHealth;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            GetComponent<BaseStats>().onLevelUp += UpdateHealth;
        }

        void Update()
        {
            
        }

        public void UpdateHealth()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health); //performance hit? How expensive is this operation?
            currentHealth = maxHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damageToTake)
        {
            if (!IsDead())
            {
                bloodFX.Play();
                currentHealth -= damageToTake;
                Debug.Log(gameObject.name + " has taken " + damageToTake + " damage");
                if (currentHealth <= 0)
                {
                    Die();
                    AwardExperience(instigator);
                }
                else
                {
                    int takeDamageSFXIndex = UnityEngine.Random.Range(0, takeDamageClips.Length);
                    audioSource.PlayOneShot(takeDamageClips[takeDamageSFXIndex]);
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("TakeDamage");
                }
            }
            else
            {
                return;
            }
        }

        public float GetPercentage() 
        {
            return 100 * (currentHealth / maxHealth);
        }

/*        public void SetHealthToMax() //have no idea if I'll be using this.
        {
            currentHealth = maxHealth;
        }*/

        private void Die()
        {
            currentHealth = 0f;
            animator.SetTrigger("Die"); //might want to substitute this with a ragdoll?
            audioSource.PlayOneShot(deathSFX);
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator) 
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        
        private void DieBetweenScenes() //this is called ONLY if the character is already dead.
        {
            currentHealth = 0f;
            animator.SetTrigger("Die"); //might want to substitute this with a ragdoll?
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void Cancel()
        {
            
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            //restore healthPoints
            currentHealth = (float)state;

            //possibly die
            if(currentHealth <= 0) 
            {
                DieBetweenScenes();
            }
        }
    }
}

