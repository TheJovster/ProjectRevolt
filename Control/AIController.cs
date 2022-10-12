using UnityEngine;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;
using ProjectRevolt.Core;

namespace ProjectRevolt.Control 
{
    public class AIController : MonoBehaviour
    {
        //variables
        private Vector3 guardPosition;

        [SerializeField] private float chaseDistance;
        [SerializeField] private float timeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField] private float suspicionTime = 5f;

        //components
        private Fighter fighter;
        private Mover mover;
        private Health health;

        //game objects
        private GameObject player;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        void Update()
        {
            //timer
            if (health.IsDead()) //if actor is dead
            {
                return;
            }
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) //attack and chase behaviour
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime) //suspicion behaviour
            {
                SuspicionBehaviour();
            }
            else //return to original position/route
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;

        }

        //behaviours

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        //calculations
        private bool InAttackRangeOfPlayer() 
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            
        }
    }

}