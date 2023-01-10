using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue testDialogue;
        private Dialogue currentDialogue;
        private DialogueNode currentNode = null;
        private bool isChoosing = false;

        public event Action onConversationUpdated;

        private IEnumerator Start() 
        {
            yield return new WaitForSeconds(2f);
            if(testDialogue != null)
            StartDialogue(testDialogue);
        }

        public bool IsActive() 
        {
            return currentDialogue != null;
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            onConversationUpdated();

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
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            currentNode = children[randomIndex];
            onConversationUpdated();
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

        public void Quit()
        {
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }
    }
}




