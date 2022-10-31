using UnityEngine;
using UnityEngine.EventSystems;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;
using ProjectRevolt.Core;
using ProjectRevolt.Attributes;
using System;

namespace ProjectRevolt.Control 
{
    public class PlayerController : MonoBehaviour
    {
        enum CursorType 
        {
            None,
            Movement,
            Combat,
            UI

        }
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

        [SerializeField] CursorMapping[] cursorMappings = null;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithCombat() 
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!fighter.CanAttack(target.gameObject)) 
                {
                    continue;
                }
                if(Input.GetMouseButton(0)) 
                {
                    fighter.Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButton(0)) 
                {
                    mover.StartMoveAction(hit.point);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool InteractWithUI()
        {
            
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
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

