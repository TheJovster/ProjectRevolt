using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectRevolt.SceneManagement 
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private Coroutine currentlyActiveFade = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private IEnumerator FadeRoutine(float target, float time) 
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {

                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

        public Coroutine Fade(float target, float time) 
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveFade;
        }

        public Coroutine FadeIn(float time) 
        {
            return Fade(0, time);
        }

        public Coroutine FadeOut(float time) 
        {
            return Fade(1, time);
        }

        public void FadeOutImmediate() 
        {
            canvasGroup.alpha = 1;
        }

    }
}

