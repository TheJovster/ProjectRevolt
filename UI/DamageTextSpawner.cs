using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.UI.DamageText 
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        private void Start()
        {
            Spawn(5f);
        }

        public void Spawn(float damageAmount) 
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}
