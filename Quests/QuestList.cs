using GameDevTV.Inventories;
using ProjectRevolt.Core;
using ProjectRevolt.Saving;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace ProjectRevolt.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        private List<QuestStatus> statuses = new List<QuestStatus>();
        public event Action onUpdate;

        void Update() 
        {
            Debugger();
        }

        public void AddQuest(Quest quest)
        {
            if(HasQuest(quest)) return;
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            if (onUpdate != null) 
            {
                onUpdate();
            }
        }
        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (status.IsComplete()) 
            {
                GiveReward(quest);
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        private QuestStatus GetQuestStatus(Quest quest) 
        {
            foreach(QuestStatus status in statuses) 
            {
                if(status.GetQuest() == quest) 
                {
                    return status;
                }
            }
            return null;

        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach(QuestStatus status in statuses) 
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            statuses.Clear();
            foreach(object objectState in stateList) 
            {
               statuses.Add(new QuestStatus(objectState));
            }
            onUpdate();
        }

        private void GiveReward(Quest quest)
        {
            foreach(var reward in quest.GetRewards()) 
            {
                bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                if (!success) 
                {
                    GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }

        private void Debugger() 
        {
            if (Input.GetKeyDown(KeyCode.U)) 
            {
                Debug.Log(statuses.Count);
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if(predicate != "HasQuest") 
            {
                return null;
            }
            return HasQuest(Quest.GetByName(parameters[0]));
        }
    }
}
