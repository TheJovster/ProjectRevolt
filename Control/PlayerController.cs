using UnityEngine;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;

namespace ProjectRevolt.Control 
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;

        void Start()
        {
            mover = GetComponent<Mover>();
        }
        void Update()
        {
            InteractWithMovement();
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mover.MoveTo(hit.point);
            }
        }
    }
}

