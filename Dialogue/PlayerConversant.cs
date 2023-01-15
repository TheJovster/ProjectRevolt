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

        [SerializeField] private string playerName;
        private Dialogue currentDialogue;
        private DialogueNode currentNode = null;
        private AIConversant currentConversant = null;
        private bool isChoosing = false;

        public event Action onConversationUpdated;

        private IEnumerator Start() 
        {
            yield return new WaitForSeconds(2f);
        }

        public bool IsActive() 
        {
            return currentDialogue != null;
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
            newConversant.PlayCharacterSpeech();

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


        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void SelectChoice(DialogueNode choice)
        {
            currentNode = choice;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }
        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        public string GetCurrentConversantName() 
        {
            if (isChoosing) return playerName;
            else return currentConversant.GetCharacterName();
        }

        private void TriggerEnterAction() 
        {
            if(currentNode != null) 
            {
                TriggerAction(currentNode.GetEnterAction());
            }
        }

        private void TriggerExitAction() 
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetExitAction());
            }
        }

        private void TriggerAction(string action) 
        {
            if(action == "") 
            {
                return;
            }
            foreach (DialogueTrigger triggers in currentConversant.GetComponents<DialogueTrigger>()) 
            {
                triggers.Trigger(action);
            }
        }

        public void Quit()
        {

            currentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated();
        }
    }
}




