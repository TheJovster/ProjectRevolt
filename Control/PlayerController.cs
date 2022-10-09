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
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat() 
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
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    mover.MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

