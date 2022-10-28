using ProjectRevolt.Core;
using UnityEditor;
using UnityEngine;

namespace ProjectRevolt.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Data")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage = 0f;
        [SerializeField] private bool isHoming = false;
        [SerializeField] private ParticleSystem impactFX;
        [SerializeField] private float maxLifeTime = 10f;
        //[SerializeField] private GameObject[] destroyOnImpact = null;
        //[SerializeField] private float lifeAfterImpact = 2f;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] audioClips;
        
        private Health target;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            
        }

        public void SetTarget(Health target, float damage) 
        {
            this.target = target;
            this.damage = damage;

            Destroy(this.gameObject, maxLifeTime);
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
/*            if (other.GetComponent<Health>() == null) //impact event for non-enemy objects - BUG: The arrows get destroyed upon contact with the bow. Need to figure out a fix.
            {
                ParticleSystem impactFXInstanceGeneric = Instantiate(impactFX, transform.position, Quaternion.identity);
                Destroy(impactFXInstanceGeneric.gameObject, impactFX.main.duration);
                Destroy(this.gameObject);//destroy self
            }*/
            if (other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;

            //impact event for enemy objects
            int impactSFXIndex = Random.Range(0, audioClips.Length);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[impactSFXIndex]);
            Debug.Log(other.transform.name + " hit!");
            target.TakeDamage(damage);//deal damage
            ParticleSystem impactFXInstance = Instantiate(impactFX, other.transform.position + Vector3.up, Quaternion.identity);
            Destroy(impactFXInstance.gameObject, impactFX.main.duration);
            Destroy(this.gameObject);//destroy self
        }

        private void OnBecameInvisible()
        {
            Destroy(this.gameObject,.5f);
        }
    }
}

