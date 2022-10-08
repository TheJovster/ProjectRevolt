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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void MoveToCursor() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) 
        {
            navMeshAgent.destination = hit.point;
            
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float moveSpeed = localVelocity.z;
        animator.SetFloat("MoveSpeed", moveSpeed, 0.1f, Time.deltaTime); //this does work
    }
}
