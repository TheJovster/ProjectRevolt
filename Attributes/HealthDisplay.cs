using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.Attributes 
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField] private TextMeshProUGUI healthValue;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            
        }

        void Start()
        {

        }

        void Update()
        {
            healthValue.text = string.Format("{0:0}%", health.GetPercentage().ToString());
        }
    }
}
