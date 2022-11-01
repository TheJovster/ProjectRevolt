using UnityEngine;
using ProjectRevolt.Core;
using ProjectRevolt.Movement;
using ProjectRevolt.Attributes;
using ProjectRevolt.Saving;
using ProjectRevolt.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using Unity.VisualScripting;

namespace ProjectRevolt.Combat 
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] private float timeBetweenAttacks = .75f;
        private float timeSinceLastAttack = Mathf.Infinity;

        //weapon
        [Header("Weapon Scriptable Object")]
        [SerializeField] private WeaponConfig defaultWeaponConfig = null;
        WeaponConfig currentWeaponConfig = null;
        LazyValue<Weapon> currentWeapon;

        [Header("Hand Transforms")]
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;

        private Health target;
        private Animator animator; //is this redundant?
        private Mover mover;
        private ActionScheduler actionScheduler;

        [Header("Audio")]
        [SerializeField] private AudioSource weaponAudioSource;

        private void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            currentWeaponConfig = defaultWeaponConfig;
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        }



        private Weapon SetupDefaultWeapon() 
        {
            return AttachWeapon(defaultWeaponConfig);
        }
        private void Start()
        {
            currentWeapon.ForceInit();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
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

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeapon.value = AttachWeapon(weapon); 
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            return  weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget() 
        {
            return target;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeaponConfig.GetWeaponRange();
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage) 
            {
                yield return currentWeaponConfig.GetWeaponDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
            }
        }


        //Animation event
        private void Hit()
        {
            if (target == null) return;

            if(currentWeapon.value != null) 
            {
                currentWeapon.value.OnHit();
            }

            float damageToTake = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeaponConfig.HasProjectile()) 
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damageToTake);
                //instantiates projectile - set target
                //object pooling?
            }
            else 
            {
                target.TakeDamage(gameObject, damageToTake);
                //audio?
            }
            if (target.GetComponent<Health>().IsDead())
            {
                Cancel();
            }
        }

        private void Shoot() 
        {
            Hit();
        }
        //ISaveable interface implementation - basic iteration
        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }


    }
}

