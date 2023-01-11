using UnityEngine;
using ProjectRevolt.Quests;
using TMPro;


namespace ProjectRevolt.UI.Quests 
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;

        private QuestStatus questStatus;

        public void Setup(QuestStatus questStatus)
        {
            this.questStatus = questStatus;
            title.text = questStatus.GetQuest().GetTitle();
            progress.text = questStatus.GetCompletedCount() + "/" + questStatus.GetQuest().GetObjectiveCount();
        }

        public QuestStatus GetQuestStatus()
        {
            return questStatus;
        }
    }
}

