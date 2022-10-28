using ProjectRevolt.Saving;
using UnityEngine;

namespace ProjectRevolt.Attributes 
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float experiencePoints = 0;



        public void GainExperience(float experience) 
        {
            experiencePoints += experience;
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
