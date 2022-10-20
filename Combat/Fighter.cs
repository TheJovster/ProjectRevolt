using UnityEngine;
using ProjectRevolt.Core;
using ProjectRevolt.Movement;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ProjectRevolt.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float timeBetweenAttacks = .75f;
        private float timeSinceLastAttack = Mathf.Infinity;

        //weapon
        [Header("Weapon Scriptable Object")]
        [SerializeField] private Weapon weapon = null;

        [Header("Hand Transforms")]
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;

        private Health target;
        private Animator animator; //is this redundant?
        private Mover mover;
        private ActionScheduler actionScheduler;

        //sound manager
        private AudioSource audioSource;

        private void Start()
        {
            

            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            SpawnWeapon();
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
        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public bool CanAttack(GameObject combatTarget) 
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
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
            //more stuff to add
        }

        private void SpawnWeapon()
        {
            if (weapon == null) return;
            weapon.Spawn(rightHandTransform, animator);

            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetWeaponRange();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        //Animation triggers
        private void TriggerAttack()
        {
            animator.ResetTrigger("StopAttack");
            animator.SetTrigger("Attack");
        }

        private void StopAttack()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }


        //Animation event
        private void Hit()
        {
            if (target == null) return;
            Debug.Log("You hit the enemy with your club. That's gotta hurt!");
            target.TakeDamage(weapon.GetWeaponDamage());
            weapon.GetPitchLevel();
            weapon.GetVolumeLevel();
            audioSource.PlayOneShot(weapon.HitFXToPlay());
            if (target.GetComponent<Health>().IsDead())
            {
                Cancel();
            }
        }

        private void Swing() 
        {
            weapon.GetPitchLevel();
            weapon.GetVolumeLevel();
            audioSource.PlayOneShot(weapon.SwingFXToPlay());
        }
    }
}

