using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    //navigation
    private NavMeshAgent navMeshAgent;
    


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            MoveToCursor();
        }

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
