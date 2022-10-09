using UnityEngine;
using ProjectRevolt.Core;
using ProjectRevolt.Movement;

namespace ProjectRevolt.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = .75f;
        private float timeSinceLastAttack;

        private Transform target;
        private Animator animator; //is this redundant?
        private Mover mover;
        private ActionScheduler actionScheduler;

        private void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack >= timeBetweenAttacks) 
            {
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0f;
            }
            //more stuff to add
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) 
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel() 
        {
            target = null;
        }


        //Animation event
        private void Hit()
        {
            Debug.Log("You hit the enemy with your club. That's gotta hurt!");
        }

    }
}

