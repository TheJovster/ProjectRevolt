using GameDevTV.Inventories;
using ProjectRevolt.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectRevolt.Quests 
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ProjectRevolt/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>();

        [Serializable]
        public class Reward 
        {
            [Min(1)]
            public int number;
            public InventoryItem item;
        }
        [Serializable]
        public class Objective 
        {
            public string reference;
            public string description;
            public bool usesCondition = false;
            public Condition completionCondition;
        }

        public string GetTitle() 
        {
            return name;
        }

        public int GetObjectiveCount() 
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives() 
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards() 
        {
            return rewards;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach(var objective in objectives) 
            {
                if(objective.reference == objectiveRef) 
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName) 
        {
            foreach(Quest quest in Resources.LoadAll<Quest>("")) 
            {
                if(quest.name == questName) 
                {
                    return quest;
                }
            }
            return null;
        }
    }
}