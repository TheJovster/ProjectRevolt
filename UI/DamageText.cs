using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectRevolt.UI.DamageText 
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text = null;

        public void DestroyText() 
        {
            Destroy(gameObject);
        }

        public void SetValue(float amount) 
        {
            text.text = string.Format("{0:0}", amount);
        }

    }
}
