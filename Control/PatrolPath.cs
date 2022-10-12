using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectRevolt.Control 
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = .3f;

        private void Start()
        {
            
        }

        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }

        }

        public int GetNextIndex(int i) 
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

