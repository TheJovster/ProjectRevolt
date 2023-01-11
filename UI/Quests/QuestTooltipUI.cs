using ProjectRevolt.Quests;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.UI.Quests 
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] Transform objectiveContainer;


        public void Setup(QuestStatus status) 
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            foreach(Transform child in objectiveContainer) 
            {
                Destroy(child.gameObject);
            }
            foreach(string objective in quest.GetObjectives()) 
            {

                GameObject prefab = objectiveIncompletePrefab;
                if (status.IsObjectiveComplete(objective)) 
                {
                    prefab = objectivePrefab;
                }
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective;
            }
            
        }
    }
}
