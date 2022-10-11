using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Control 
{
    public class AIController : MonoBehaviour
    {
        //variables
        [SerializeField] private float chaseDistance;

        //components

        //game objects

        void Start()
        {
            
        }

        void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                Debug.Log(this.name + " should give chase.");
                //add chase behaviour here
            }
        }

        //behaviour methods

        //guard behaviour

        //patrol behaviour

        //suspicion behaviour

        //chase behaviour

        //attack/aggression behaviour

        //calculations
        private float DistanceToPlayer() 
        {
            Transform player = GameObject.FindWithTag("Player").transform;
            return Vector3.Distance(player.position, transform.position);
        }
    }

}