using ProjectRevolt.Core;
using UnityEngine;

namespace ProjectRevolt.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Data")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage = 0f;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] audioClips;
        
        private Health target;

        void Update()
        {
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage) 
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation() 
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null) 
            {
                return target.transform.position;
            }
            return target.transform.position + (Vector3.up * (targetCapsule.height/2));
        }

        private void OnTriggerEnter(Collider other) //impact event
        {
            if (other.GetComponent<Health>() != target) return;
            else if(other.GetComponent<Health>() == target) 
            {
                int impactSFXIndex = Random.Range(0, audioClips.Length);
                other.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[impactSFXIndex]);
                Debug.Log(other.transform.name + " hit!");
                target.TakeDamage(damage);//deal damage
                Destroy(this.gameObject);//destroy self
            }
        }
    }
}

