using UnityEngine;

namespace ProjectRevolt.Core 
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction = null;

        void Start()
        {

        }

        void Update()
        {

        }

        public void StartAction(MonoBehaviour action) 
        {
            if (currentAction == action) return;
            if(currentAction != null) 
            {
                Debug.Log(currentAction + " cancelled");
            }
            currentAction = action;
        }
    }
}

