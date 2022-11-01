using ProjectRevolt.Attributes;
using UnityEngine;

namespace ProjectRevolt.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
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
        [SerializeField] private Weapon equippedPrefab = null;

        //animation overrides
        [Header("Animator Override Controller")]
        [SerializeField] private AnimatorOverrideController weaponOverrideController;

       

        //weapon icon for the ui?
        //weapon value for the store?

        public Weapon Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator) 
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            Weapon weapon = null;

            if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
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

            return weapon;
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
