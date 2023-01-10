using ProjectRevolt.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;

        public string GetText()
        {
            if(currentDialogue == null) 
            {
                return "No Dialogue to display. This is a dev message. If you're seeing this as a tester, that means there's a bug.";
            }
            return currentDialogue.GetRootNode().GetText();
        }

        private void Next()
        {

        }
        private string[] GetChoices()
        {
            return null;
        }

        private void SelectChoice(string choice)
        {

        }

        private bool IsChoosing()
        {
            return false;
        }

        private bool HasNext()
        {
            return false;
        }

        private void StartDialogue(Dialogue dialogue)
        {

        }

        private void Quit()
        {

        }
    }
}




