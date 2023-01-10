using ProjectRevolt.Dialogue;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.UI 
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant playerConversant;
        //[SerializeField] private TextMeshProUGUI conversantName;
        [SerializeField] private TextMeshProUGUI AIText;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            //
            AIText.text = playerConversant.GetText();
        }

        void Update()
        {

        }
    }
}
