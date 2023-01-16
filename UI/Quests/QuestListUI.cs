using UnityEngine;
using ProjectRevolt.Quests;

namespace ProjectRevolt.UI.Quests 
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private QuestItemUI questPrefab;
        [SerializeField] private AudioClip questAdded;
        private QuestList questList;
        // Start is called before the first frame update
        private void Start()
        {
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.onUpdate += PlayOneShotAddedQuest;
            questList.onUpdate += Redraw;
            Redraw();
        }

        void Redraw()
        {
            foreach(Transform child in transform) 
            {
                Destroy(child.gameObject);
            }
            foreach (QuestStatus status in questList.GetStatuses()) 
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }

        private void PlayOneShotAddedQuest() 
        {
            GameObject.FindGameObjectWithTag("SceneFXManager").GetComponent<AudioSource>().PlayOneShot(questAdded);

        }
    }
}

