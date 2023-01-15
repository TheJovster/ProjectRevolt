using ProjectRevolt.Quests;
using System;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.UI.Quests 
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI rewardText;
        [SerializeField] GameObject objectiveCompletePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] Transform objectiveContainer;


        public void Setup(QuestStatus status) 
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            foreach (Transform child in objectiveContainer)
            {
                Destroy(child.gameObject);
            }
            foreach (var objective in quest.GetObjectives()) 
            {
                GameObject prefab = objectiveIncompletePrefab; ;
                Debug.Log("objective not complete");
                if (status.IsObjectiveComplete(objective.reference)) 
                {
                    prefab = objectiveCompletePrefab;
                    Debug.Log("Objective complete");
                }
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
                //debugging
                Debug.Log("QuestTooltipUI updated");
            }
            rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach(var reward in quest.GetRewards()) 
            {
                if(rewardText != "") 
                {
                    rewardText += ", ";
                }
                if(reward.number > 1) 
                {
                    rewardText += reward.number + " ";
                }
                rewardText += reward.item.GetDisplayName();
            }
            if(rewardText == "") 
            {
                rewardText = "No reward";
            }
            rewardText += ".";
            return rewardText;
        }
    }
}
