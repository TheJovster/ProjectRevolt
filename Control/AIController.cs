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
        [SerializeField] private float aggroCooldownTime = 5f;


        [Tooltip("The radius in which the enemy mobs start attacking the player")]
        [SerializeField] private float globalAggroRadius;

        [SerializeField] private float waypointTolerance = 1f;
        

        private int currentWaypointIndex = 0;
        private float timeSpentAtWaypoint = Mathf.Infinity;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceAggrevated = Mathf.Infinity;

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
            if (IsAggravated() && fighter.CanAttack(player)) //attack and chase behaviour
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

            AggravateNearbyEnemies();
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

        private void AggravateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, globalAggroRadius, Vector3.up, 0f);
            foreach(RaycastHit hit in hits) 
            {
                AIController aiController = hit.collider.GetComponent<AIController>();
                if(aiController == null) 
                {
                    continue;
                }
                aiController.Aggrevate();
            }
        }

        public void Aggrevate() 
        {
            timeSinceAggrevated = 0f;
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
        private bool IsAggravated() 
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance || timeSinceAggrevated < aggroCooldownTime;
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
            timeSinceAggrevated += Time.deltaTime;
        }
    }

}