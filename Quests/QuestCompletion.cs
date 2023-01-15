using UnityEngine;

namespace ProjectRevolt.Quests 
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField]private Quest quest;
        [Tooltip("The objective string is the same as the reference string on the Quest Scriptable Object.")]
        [SerializeField]private string objective;

        public void CompleteObjective() 
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.CompleteObjective(quest, objective);
            Debug.Log("Added " + objective + " to quest completion status");
        }
    }
}
