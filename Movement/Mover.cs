using UnityEngine;
using UnityEngine.AI;
using ProjectRevolt.Core;
using ProjectRevolt.Saving;
using ProjectRevolt.Attributes;

namespace ProjectRevolt.Movement 
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        //variables
        //components
        private Animator animator;
        private ActionScheduler actionScheduler;
        private Health health;

        //navigation
        private NavMeshAgent navMeshAgent;

        //sound
        private AudioSource audioSource;
        [SerializeField] private AudioClip[] footsteps;
        [Range(0.1f, 0.5f)][SerializeField] private float volumeChangeMultiplier = .2f;
        [Range(0.1f, 0.5f)][SerializeField] private float pitchChangeMultiplier = .2f;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
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

        //animation events
        private void Step() 
        {
            int clipIndex = Random.Range(0, footsteps.Length);
            audioSource.volume = Random.Range(1 - volumeChangeMultiplier, 1);
            audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1);
            audioSource.PlayOneShot(footsteps[clipIndex]);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}

