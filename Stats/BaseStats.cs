using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProjectRevolt.Stats 
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)][SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;
        [SerializeField] private bool shouldUseModifiers = false;


        //actions and delegates
        public event Action onLevelUp;

        [Header("Visual and Sound FX")]
        [SerializeField] private ParticleSystem levelUpVFX;
        [SerializeField] private AudioClip levelUpSFX;
        private int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null) 
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel) 
            {
                //level up event
                currentLevel = newLevel;
                onLevelUp.Invoke();
                levelUpVFX.Play();
                GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(levelUpSFX);
                onLevelUp();
            }
            
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }



        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel() 
        {
            if(currentLevel < 1) 
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>()) 
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat)) 
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }


        private int CalculateLevel() 
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetExperiencePoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++) 
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(xpToLevelUp > currentXP) 
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
    }
}