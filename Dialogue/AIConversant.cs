using ProjectRevolt.Control;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectRevolt.Dialogue 
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Dialogue conversantDialogue;
        [SerializeField] private float minimumDistance = 5f;
        
        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(conversantDialogue == null) 
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0) && DistanceToPlayer() <= minimumDistance) 
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, conversantDialogue);
            }
            return true;
        }

        private float DistanceToPlayer() 
        {
             return Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
