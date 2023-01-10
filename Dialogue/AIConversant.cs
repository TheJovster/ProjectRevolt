using ProjectRevolt.Attributes;
using ProjectRevolt.Control;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectRevolt.Dialogue 
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Dialogue conversantDialogue;
        [SerializeField] private float minimumDistance = 5f;
        [SerializeField] private string characterName;
        
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
                if (GetComponent<Health>() != null && !GetComponent<Health>().IsDead()) 
                {
                    callingController.GetComponent<PlayerConversant>().StartDialogue(this, conversantDialogue);
                }
                else 
                {
                    callingController.GetComponent<PlayerConversant>().StartDialogue(this, conversantDialogue);
                }

            }
            return true;
        }

        public string GetCharacterName() 
        {
            return characterName;
        }

        private float DistanceToPlayer() 
        {
             return Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
