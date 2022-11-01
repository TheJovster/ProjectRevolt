using UnityEngine;
using UnityEngine.UI;

namespace ProjectRevolt.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform healthBarFill = null;
        [SerializeField] private Health health = null;

        // Update is called once per frame
        void Update()
        {

            if (Mathf.Approximately(health.GetFraction(), 0f) || Mathf.Approximately(health.GetFraction(), 1f))
            {
                canvas.enabled = false;
                return;
            }
            canvas.enabled = true;
            healthBarFill.localScale = new Vector3(health.GetFraction(), 1f, 1f);

        }
    }
}

