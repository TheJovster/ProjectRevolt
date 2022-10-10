using UnityEngine;
using ProjectRevolt.Core;
using ProjectRevolt.Movement;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ProjectRevolt.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private float timeBetweenAttacks = .75f;
        private float timeSinceLastAttack;

        private Health target;
        private Animator animator; //is this redundant?
        private Mover mover;
        private ActionScheduler actionScheduler;

        //sound manager
        private AudioSource audioSource;
        [Header("Audio and Sound")]
        [SerializeField]private AudioClip[] swingEffects;
        [SerializeField]private AudioClip[] hitEffects;
        [Range(.1f, .5f)][SerializeField] private float volumeChangeMultiplier = .2f;
        [Range(.1f, .5f)][SerializeField] private float pitchChangeMultiplier = .2f;


        private void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public bool CanAttack(CombatTarget combatTarget) 
        {
            if (combatTarget == null) return false;

            Health combatTargetToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && !combatTargetToTest.IsDead();
        }

        private void AttackBehaviour()
        {
            Vector3 lookAtPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(lookAtPosition);
            if (timeSinceLastAttack > timeBetweenAttacks) 
            {
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0f;
            }
            //more stuff to add
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public void Cancel() 
        {
            animator.SetTrigger("StopAttack");
            animator.ResetTrigger("StopAttack");
            target = null;
        }


        //Animation event
        private void Hit()
        {
            Debug.Log("You hit the enemy with your club. That's gotta hurt!");
            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }
            else return;
            int hitSFXIndex = Random.Range(0, swingEffects.Length);
            audioSource.volume = Random.Range(1 - volumeChangeMultiplier, 1);
            audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1);
            audioSource.PlayOneShot(hitEffects[hitSFXIndex]);
            if (target.GetComponent<Health>().IsDead())
            {
                Cancel();
            }
        }

        private void Swing() 
        {
            int swingSFXIndex = Random.Range(0, swingEffects.Length);
            audioSource.volume = Random.Range(1 - volumeChangeMultiplier, 1);
            audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1);
            audioSource.PlayOneShot(swingEffects[swingSFXIndex]);
        }
    }
}

