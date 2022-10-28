using ProjectRevolt.Saving;
using UnityEngine;
using ProjectRevolt.Stats;
using ProjectRevolt.Core;

namespace ProjectRevolt.Attributes 
{
    public class Health : MonoBehaviour, IAction, ISaveable
    {
        [Header("Health Variables")]
        [SerializeField]private float maxHealth = 50f;
        [SerializeField]private float currentHealth;
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

        private void Awake()
        {
            maxHealth = GetComponent<BaseStats>().GetHealth();
            currentHealth = maxHealth;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
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
                    animator.ResetTrigger("Attack");
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

