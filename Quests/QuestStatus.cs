using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Quests 
{
    public class QuestStatus 
    {
        private Quest quest;
        private List<string> completedObjectives = new List<string>();

        [Serializable] public class QuestStatusRecord 
        {
            public string questName;
            public List<string> completedObjectives;
        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public QuestStatus(object objectState)
        {
            QuestStatusRecord state = objectState as QuestStatusRecord;
            quest = Quest.GetByName(state.questName);
            completedObjectives = state.completedObjectives;
        }

        public Quest GetQuest() 
        {
            return quest;
        }

        public int GetCompletedCount() 
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective) 
        {
            return completedObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective)) 
            {
                completedObjectives.Add(objective);
            }
        }

        public object CaptureState()
        {
            QuestStatusRecord state = new QuestStatusRecord();
            state.questName = quest.name;
            state.completedObjectives = completedObjectives;
            return state;
        }

        public bool IsComplete()
        {
            foreach(var objective in quest.GetObjectives()) 
            {
                if (!completedObjectives.Contains(objective.reference)) return false;
            }
            return true;
        }
    }
}
