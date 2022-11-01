using ProjectRevolt.Saving;
using UnityEngine;
using ProjectRevolt.Stats;
using ProjectRevolt.Core;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace ProjectRevolt.Attributes 
{
    public class Health : MonoBehaviour, IAction, ISaveable
    {
        [Header("Health Variables")]
        [SerializeField]private LazyValue<float> healthPoints;
        private bool isDead = false;

        [SerializeField] TakeDamageEvent takeDamage;

        [Serializable]
        public class TakeDamageEvent : UnityEvent<float> 
        {
        }

        //delegates and events

        //animation
        private Animator animator;

        //Audio
        [Header("Audio")]
        [SerializeField] private AudioSource damageAudioSource;
        [SerializeField] private AudioClip[] takeDamageClips;
        [SerializeField] private AudioClip deathSFX;

        //visual FX
        [Header("VFX")]
        [SerializeField] private ParticleSystem bloodFX;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth() 
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += UpdateHealth;
            animator = GetComponent<Animator>();
            healthPoints.ForceInit();
        }

        void Update()
        {
            
        }

        public void UpdateHealth()
        {
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void Heal(float healAmount) 
        {
            healthPoints.value += healAmount;
            if(healthPoints.value > GetComponent<BaseStats>().GetStat(Stat.Health)) 
            {
                healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public void TakeDamage(GameObject instigator, float damageToTake)
        {
            if (!IsDead())
            {
                bloodFX.Play();
                healthPoints.value -= damageToTake;
                takeDamage.Invoke(damageToTake);
                Debug.Log(gameObject.name + " has taken " + damageToTake + " damage");
                if (healthPoints.value <= 0)
                {
                    Die();
                    AwardExperience(instigator);
                }
                else
                {
                    int takeDamageSFXIndex = UnityEngine.Random.Range(0, takeDamageClips.Length);
                    damageAudioSource.PlayOneShot(takeDamageClips[takeDamageSFXIndex]);
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
            return 100 * GetFraction();
        }

/*        public void SetHealthToMax() //have no idea if I'll be using this.
        {
            currentHealth = maxHealth;
        }*/

        public float GetFraction() 
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            healthPoints.value = 0f;
            damageAudioSource.PlayOneShot(deathSFX);
            GetComponent<Animator>().SetTrigger("Die"); //might want to substitute this with a ragdoll?
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
            healthPoints.value = 0f;
            GetComponent<Animator>().SetTrigger("Die"); //might want to substitute this with a ragdoll?
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void Cancel()
        {
            
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            
            //restore healthPoints
            healthPoints.value = (float)state;

            //possibly die
            if(healthPoints.value <= 0) 
            {
                DieBetweenScenes();
            }
        }
    }
}

