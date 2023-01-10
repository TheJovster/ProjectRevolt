using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Combat 
{
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] private Fighter[] fighters;
        [SerializeField] private bool activateOnStart = false;

        private void Start()
        {
            Activate(activateOnStart);   
        }

        public void Activate(bool shouldActivate) 
        {
            foreach(var fighter in fighters) 
            {
                CombatTarget target = fighter.GetComponent<CombatTarget>();
                if(target != null) 
                {
                    target.enabled = shouldActivate;
                }
                fighter.enabled = shouldActivate;
            }
        }
    }
}
