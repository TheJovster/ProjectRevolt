using UnityEngine;

namespace ProjectRevolt.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        //variables
        [Header("Weapon Variables")]
        [SerializeField] private float weaponDamage;
        [SerializeField] private float weaponRange;

        //visual for the object
        [Header("Weapon Visual")]
        [SerializeField] private GameObject weaponPrefab = null;

        //animation overrides
        [Header("Animator Override Controller")]
        [SerializeField] private AnimatorOverrideController weaponOverrideController;

        //sound

        public void Spawn(Transform handTransform, Animator animator) 
        {
            if(weaponPrefab != null) 
            {
                Instantiate(weaponPrefab, handTransform);
            }
            animator.runtimeAnimatorController = weaponOverrideController;
        }

    }
}
