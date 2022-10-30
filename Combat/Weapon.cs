using ProjectRevolt.Attributes;
using UnityEngine;

namespace ProjectRevolt.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        //variables
        [Header("Weapon Variables and Components")]
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private float percentageBonus = 0f;
        [SerializeField] private Projectile projectile = null;

        const string weaponName = "Weapon";
        //weapon speed?
        //which hand should be used
        [Header("Conditionals")]
        [SerializeField] private bool isLeftHanded = false;

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

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator) 
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if(weaponPrefab != null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (weaponOverrideController != null) 
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null) 
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null)
            {
                return;
            }
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isLeftHanded)
            {
                handTransform = leftHandTransform;
            }
            else
            {
                handTransform = rightHandTransform;
            }

            return handTransform;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage) 
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }
        //getters
        public float GetWeaponDamage() 
        {
            return weaponDamage;
        }

        public float GetPercentageBonus() 
        {
            return percentageBonus;
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

        public bool IsLeftHanded() 
        {
            return isLeftHanded;
        }

        public bool HasProjectile() 
        {
            return projectile != null;
        }
    }
}
