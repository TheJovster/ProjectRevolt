using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Quests 
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ProjectRevolt/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives;

        public string GetTitle() 
        {
            return name;
        }

        public int GetObjectiveCount() 
        {
            return objectives.Length;
        }

        public IEnumerable<string> GetObjectives() 
        {
            return objectives;
        }
        
    }
}