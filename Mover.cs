using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }
}
