using UnityEngine;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;

namespace ProjectRevolt.Control 
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void InteractWithCombat() 
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (target == null) continue;

                if(Input.GetMouseButtonDown(0)) 
                {
                    fighter.Attack(target);
                } 
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                mover.MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

