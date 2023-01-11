using GameDevTV.Core.UI.Tooltips;
using ProjectRevolt.Quests;
using UnityEngine;

namespace ProjectRevolt.UI.Quests 
{
        public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            QuestStatus questStatus = GetComponent<QuestItemUI>().GetQuestStatus();
            tooltip.GetComponent<QuestTooltipUI>().Setup(questStatus);
        }
    }
}
