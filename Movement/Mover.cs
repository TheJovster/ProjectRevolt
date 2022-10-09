using UnityEngine;
using UnityEngine.AI;
using ProjectRevolt.Core;

namespace ProjectRevolt.Movement 
{
    public class Mover : MonoBehaviour, IAction
    {
        //variables
        //components
        [HideInInspector] public Animator animator;
        private ActionScheduler actionScheduler;

        //navigation
        private NavMeshAgent navMeshAgent;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) 
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel() 
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

