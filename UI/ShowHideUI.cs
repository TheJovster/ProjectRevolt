using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uiContainer = null;

        void Start()
        {
            uiContainer.SetActive(false);
        }
        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}
