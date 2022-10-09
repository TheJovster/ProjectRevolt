using UnityEngine;
using UnityEngine.AI;
using ProjectRevolt.Combat;

namespace ProjectRevolt.Movement 
{
    public class Mover : MonoBehaviour
    {
        //variables
        //components
        private Animator animator;
        private Fighter fighter;

        //navigation
        private NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) 
        {
            fighter.Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
            fighter.Cancel();
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
            animator.SetFloat("MoveSpeed", moveSpeed, 0.1f, Time.deltaTime); //this does work
        }
    }
}

