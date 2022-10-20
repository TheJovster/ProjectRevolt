using ProjectRevolt.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            other.GetComponent<Fighter>().EquipWeapon(weapon);
            Destroy(this.gameObject);
        }
    }
}
