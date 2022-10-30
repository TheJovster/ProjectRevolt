using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectRevolt.SceneManagement 
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Update()
        {

        }

        public IEnumerator FadeOut(float time)
        {
            while(canvasGroup.alpha < 1)
            {
                //float timeToFadeout = 1 / (time / Time.deltaTime);
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }

        }

        public IEnumerator FadeIn(float time) 
        {
            while(canvasGroup.alpha > 0) 
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            
        }

        public void FadeOutImmediate() 
        {
            canvasGroup.alpha = 1;
        }

    }
}

