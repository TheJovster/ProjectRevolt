using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Core 
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 cameraOffset;

        void Start()
        {

        }

        void LateUpdate()
        {
            transform.position = target.position + cameraOffset;
        }
    }
}

