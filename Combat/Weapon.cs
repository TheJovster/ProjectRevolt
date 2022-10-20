using UnityEngine;

namespace ProjectRevolt.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        //variables
        [Header("Weapon Variables")]
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 20f;
        //weapon speed?

        //visual for the object
        [Header("Weapon Visual")]
        [SerializeField] private GameObject weaponPrefab = null;

        //animation overrides
        [Header("Animator Override Controller")]
        [SerializeField] private AnimatorOverrideController weaponOverrideController;

        //sound
        [Header("Audio and Sound")]
        [SerializeField] private AudioClip[] swingEffects;
        [SerializeField] private AudioClip[] hitEffects;
        [Range(.1f, .5f)][SerializeField] private float volumeChangeMultiplier = .2f;
        [Range(.1f, .5f)][SerializeField] private float pitchChangeMultiplier = .2f;

        //weapon icon for the ui?
        //weapon value for the store?

        public void Spawn(Transform handTransform, Animator animator) 
        {
            if(weaponPrefab != null) 
            {
                Instantiate(weaponPrefab, handTransform);
            }
            if(weaponOverrideController != null) 
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }
        }
        //getters
        public float GetWeaponDamage() 
        {
            return weaponDamage;
        }

        public float GetWeaponRange() 
        {
            return weaponRange;
        }
        //sound calculations

        public AudioClip HitFXToPlay() 
        {
            int hitSFXIndex = Random.Range(0, swingEffects.Length);
            return hitEffects[hitSFXIndex];
            
        }

        public AudioClip SwingFXToPlay() 
        {
            int swingSFXIndex = Random.Range(0, swingEffects.Length);
            return swingEffects[swingSFXIndex];
        }

        public float GetPitchLevel() 
        {
            return Random.Range(1 - pitchChangeMultiplier, 1);
        }

        public float GetVolumeLevel() 
        {
            return Random.Range(1 - volumeChangeMultiplier, 1);
        }

    }
}
