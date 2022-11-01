using ProjectRevolt.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace ProjectRevolt.Attributes 
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float healAmount = 5f;
        [SerializeField] private float respawnTime = 5f;
        [SerializeField] private GameObject particleFX;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player") 
            {
                Pickup(other);
                
            }
        }

        private void Pickup(Collider other)
        {
            other.GetComponent<Health>().Heal(healAmount);
            StartCoroutine(HideForSeconds(respawnTime));
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
            particleFX.SetActive(shouldShow);
        }
    }
}

