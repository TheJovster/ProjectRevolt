using UnityEngine;
using UnityEngine.UI;

namespace ProjectRevolt.Attributes 
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField] private Image healthValue;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            
        }

        void Start()
        {

        }

        void Update()
        {
            healthValue.fillAmount = health.GetFraction();
        }
    }
}
