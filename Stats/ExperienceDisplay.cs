using ProjectRevolt.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.Stats 
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI experienceValue;
        private Experience playerExperience;
        void Start()
        {
            playerExperience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            experienceValue.text = playerExperience.GetExperiencePoints().ToString();
        }
    }
}

