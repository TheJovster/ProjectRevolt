using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    //variables
    //components
    private Animator animator;

    //navigation
    private NavMeshAgent navMeshAgent;

    


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float moveSpeed = localVelocity.z;
        animator.SetFloat("MoveSpeed", moveSpeed, 0.1f, Time.deltaTime); //this does work
    }
}
