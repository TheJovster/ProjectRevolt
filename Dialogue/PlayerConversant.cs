using ProjectRevolt.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;
        private DialogueNode currentNode = null;
        private bool isChoosing = false;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }

        public bool IsChoosing() 
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode == null) 
            {
                return "No Dialogue to display. This is a dev message. If you're seeing this as a tester, that means there's a bug.";
            }
            return currentNode.GetText();
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            currentNode = children[randomIndex];
        }
        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void SelectChoice(DialogueNode choice)
        {
            currentNode = choice;
            isChoosing = false;
            Next();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        private void StartDialogue(Dialogue dialogue)
        {

        }

        private void Quit()
        {

        }
    }
}




