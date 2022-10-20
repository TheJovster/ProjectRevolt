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
        [SerializeField] private Weapon defaultWeapon;
        private Weapon currentWeapon = null;

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
            EquipWeapon(defaultWeapon);
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if (weapon.IsLeftHanded()) 
            {
                weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            }
            else if (!weapon.IsLeftHanded()) 
            {
                weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
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
            if (currentWeapon.HasProjectile()) 
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
                audioSource.PlayOneShot(currentWeapon.HitFXToPlay());
                //instantiates projectile - set target
                //object pooling?
            }
            else 
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
                currentWeapon.GetPitchLevel();
                currentWeapon.GetVolumeLevel();
                audioSource.PlayOneShot(currentWeapon.HitFXToPlay());
            }
            if (target.GetComponent<Health>().IsDead())
            {
                Cancel();
            }
        }

        private void Swing() 
        {
            currentWeapon.GetPitchLevel();
            currentWeapon.GetVolumeLevel();
            audioSource.PlayOneShot(currentWeapon.SwingFXToPlay());
        }

        private void Shoot() 
        {
            Hit();
        }
    }
}

