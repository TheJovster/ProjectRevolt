using ProjectRevolt.Attributes;
using TMPro;
using UnityEngine;


namespace ProjectRevolt.Combat 
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyHealthValue;

        Fighter playerFighter;

        private void Awake()
        {
            playerFighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if(playerFighter.GetTarget() == null) 
            {
                enemyHealthValue.text = "N/A";
                return;
            }
            Health health = playerFighter.GetTarget();
            enemyHealthValue.text = string.Format("{0:0}%", health.GetPercentage().ToString());
        }
    }
}

