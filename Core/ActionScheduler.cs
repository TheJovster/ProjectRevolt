using UnityEngine;

namespace ProjectRevolt.Core 
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction = null;

        void Start()
        {

        }

        void Update()
        {

        }

        public void StartAction(IAction action) 
        {
            if (currentAction == action) return;
            if(currentAction != null) 
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancelCurrentAction() 
        {
            StartAction(null);
        }
    }
}

