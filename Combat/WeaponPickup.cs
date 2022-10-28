using ProjectRevolt.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Combat 
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float respawnTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds) 
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);

        }

        private void ShowPickup(bool shouldShow) 
        {
            GetComponent<Collider>().enabled = shouldShow;
            GetComponent<MeshRenderer>().enabled = shouldShow;
        }
    }
}

