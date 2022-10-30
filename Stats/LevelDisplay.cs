using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelValue;
        private BaseStats baseStats;
        
        private void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        public void Update()
        {
            levelValue.text = baseStats.GetLevel().ToString();
        }

    }
}
