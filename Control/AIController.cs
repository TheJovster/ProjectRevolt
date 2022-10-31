using UnityEngine;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;
using ProjectRevolt.Core;
using ProjectRevolt.Attributes;
using System;
using GameDevTV.Utils;

namespace ProjectRevolt.Control 
{
    public class AIController : MonoBehaviour
    {
        //variables
        LazyValue<Vector3> guardPosition;

        [SerializeField] private float speedFraction = 1f;
        [SerializeField] private float chaseDistance;
        
        [SerializeField] private float suspicionTime = 5f;
        [SerializeField] private float dwellTime = 4f;
        [SerializeField] private float waypointTolerance = 1f;

        private int currentWaypointIndex = 0;
        private float timeSpentAtWaypoint = Mathf.Infinity;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        //components
        private Fighter fighter;
        private Mover mover;
        private Health health;
        [SerializeField] private PatrolPath patrolPath;

        //game objects
        private GameObject player;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private Vector3 GetGuardPosition() 
        {
            return transform.position;
        }

        void Start()
        {
            guardPosition.ForceInit();
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
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime) //suspicion behaviour
            {
                SuspicionBehaviour();
            }
            else //return to original position/route
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }


        //behaviours
        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;
            if(patrolPath != null) 
            {
                if(AtWaypoint()) 
                {
                    timeSpentAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if (timeSpentAtWaypoint > dwellTime)
            {
                mover.StartMoveAction(nextPosition, speedFraction);
            }
            
        }

        //Patrol Behaviour methods
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < waypointTolerance;
        }
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            timeSpentAtWaypoint = 0;
        }
        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
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
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSpentAtWaypoint += Time.deltaTime;
        }
    }

}