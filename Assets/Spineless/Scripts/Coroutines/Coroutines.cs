namespace Spineless
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;


    public class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;


        #region Properties
        public static Coroutines Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Global Coroutines").AddComponent<Coroutines>();
                    _instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
                }

                return _instance;
            }
        }
        #endregion


        public static IEnumerator WaitForSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            if (callback != null)
                callback();
        }


        /// <summary>
        /// Fades the specified image to the target opacity and duration.
        /// </summary>
        /// <param name="target">Target.</param>
        /// <param name="opacity">Opacity.</param>
        /// <param name="duration">Duration.</param>
        public static IEnumerator FadeImage(Image target, float duration, Color color)
        {
            if (target == null)
                yield break;

            float alpha = target.color.a;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
            {
                if (target == null)
                    yield break;
                Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
                target.color = newColor;
                yield return null;
            }

            target.color = color;
        }


        public static IEnumerator FadeImageInOut(Image target, float fadeTime, float stayTime)
        {
            target.color = Color.clear;
            yield return FadeImage(target, fadeTime, Color.white);
            yield return new WaitForSeconds(stayTime);
            yield return FadeImage(target, fadeTime, Color.clear);
        }


        public static IEnumerator FlickerRenderers(Renderer[] renderers, float duration, float speed)
        {
            int iterations = (int)((duration / 3) / speed);
            for (int i = 0; i < iterations; i++)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = false;
                }

                yield return new WaitForSeconds(speed);
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }

                yield return new WaitForSeconds(speed);
            }
        }


        public static IEnumerator OnNextFrame(Action callback)
        {
            yield return null;
            if (callback != null)
                callback();
        }


        /// <summary>
        /// Start a coroutine from global coroutine GameObject instance.
        /// Use when a Coroutine's executions shouldn't be tied to a specific object.
        /// </summary>
        /// <param name="routine">Coroutine to run.</param>
        /// <returns>Coroutine</returns>
        public static Coroutine StartGlobal(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }
    }
}