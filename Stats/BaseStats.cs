using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Stats 
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)][SerializeField] private int level = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;
        

        public float GetHealth() 
        {
            return 5f;
        }
    }
}