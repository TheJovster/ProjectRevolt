using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Combat 
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Data")]
        [SerializeField] private float moveSpeed;

        void Update()
        {
            
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        private Vector3 GetAimLocation() 
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            return target.position + (Vector3.up * 1.25f);
        }
    }
}

