using UnityEngine;
using UnityEngine.EventSystems;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;
using ProjectRevolt.Core;
using ProjectRevolt.Attributes;
using System;
using UnityEngine.AI;
using Cinemachine.Utility;
using GameDevTV.Inventories;

namespace ProjectRevolt.Control 
{
    public class PlayerController : MonoBehaviour
    {

        [System.Serializable]
        struct CursorMapping 
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }


        [SerializeField] private Camera mainCamera;
        Mover mover;
        Fighter fighter;
        Health health;
        ActionStore actionStore;

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] private float maxNavMeshProjectionDistance = 1f;
        [SerializeField] private float raycastRadius = 1f;
        [SerializeField] int numberOfAbilities = 6;

        private bool isDraggingUI = false;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            actionStore = GetComponent<ActionStore>();
        }
        void Update()
        {
            if (InteractWithUI())
            {
                return;
            }
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            UseAbilities();

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (!mover.CanMoveTo(target)) { return false; } 
                if (Input.GetMouseButton(0)) 
                {
                    if (mover.enabled == false) 
                    {
                        return false; 
                    }
                    mover.StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target) 
        {
            target = new Vector3();

            //raycast to terrain
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;
            //find nearest navmesh point
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = 
                NavMesh.SamplePosition(hit.point, out navMeshHit,  maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;
            //return true if navmesh point is found
            target = navMeshHit.position;
            //nav mesh path length calculation


            return true;
        }

        private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    actionStore.Use(i, gameObject);
                }
            }
        }

        private bool InteractWithUI()
        {
            if(Input.GetMouseButtonUp(0)) 
            {
                isDraggingUI = false;
            }
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    isDraggingUI = true;
                }
                SetCursor(CursorType.UI);
                return true;
            }
            if (isDraggingUI) 
            {
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach(RaycastHit hit in hits) 
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables) 
                {
                    if (raycastable.HandleRaycast(this)) 
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            //get all hits
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            //build distance array
            float[] distances = new float[hits.Length];
            for(int i = 0; i < hits.Length; i++) 
            {
                distances[i] = hits[i].distance;
            }
            //sort by distance
            Array.Sort(distances, hits);
            //return
            return hits;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type) 
        {
            foreach (CursorMapping mapping in cursorMappings) 
            {
                if(mapping.type == type) 
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

