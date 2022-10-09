using UnityEngine;
using UnityEngine.AI;
using ProjectRevolt.Core;
using ProjectRevolt.Combat;

namespace ProjectRevolt.Movement 
{
    public class Mover : MonoBehaviour
    {
        //variables
        //components
        private Animator animator;
        private Fighter fighter;
        private ActionScheduler actionScheduler;

        //navigation
        private NavMeshAgent navMeshAgent;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            fighter = GetComponent<Fighter>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) 
        {
            fighter.Cancel();
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop() 
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float moveSpeed = localVelocity.z;
            animator.SetFloat("MoveSpeed", moveSpeed); //this does work
        }
    }
}

