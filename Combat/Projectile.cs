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
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            transform.LookAt(target.position + (Vector3.up * 1.25f));
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}

