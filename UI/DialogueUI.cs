using ProjectRevolt.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectRevolt.UI 
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant playerConversant;
        //[SerializeField] private TextMeshProUGUI conversantName;
        [SerializeField] private TextMeshProUGUI AIText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Transform choiceRoot;
        [SerializeField] private GameObject choicePrefab;
        [SerializeField] private GameObject AIResponse;
        [SerializeField] private TextMeshProUGUI conversantName; 

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(Next);
            quitButton.onClick.AddListener(Quit); //you can also use Lambda functions for the this line and the line above

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive()) 
            {
                return;
            }
            conversantName.text = playerConversant.GetCurrentConversantName();
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else 
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComponent = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = choice.GetText();

                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => 
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }

        private void Next() 
        {
            playerConversant.Next();
        }

        private void Quit()
        {
            playerConversant.Quit();
        }

    }
}
