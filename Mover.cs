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
        if(Input.GetMouseButtonDown(0)) 
        {
            MoveToCursor();
        }
        animator.SetFloat("MoveSpeed", navMeshAgent.velocity.magnitude); //this might work
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
}
