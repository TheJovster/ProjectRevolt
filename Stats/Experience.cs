using ProjectRevolt.Saving;
using UnityEngine;
using System;

namespace ProjectRevolt.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float experiencePoints = 0;

        public event Action onExperienceGained;
        public event Action onLevelUp;

        public void GainExperience(float experience) 
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public float GetExperiencePoints() 
        {
            return experiencePoints;
        }

        //ISaveable interface implementation
        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}
