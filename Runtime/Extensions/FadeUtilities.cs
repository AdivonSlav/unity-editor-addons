using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering;


namespace FIT.EditorAddons.Extensions
{
    public static class FadeUtilities
    {
        public static IEnumerator Fade(this CanvasGroup canvasGroup,float from, float to, float time, Action callBack = null)
        {
            var WaitTime = 0.0f;
            while (WaitTime <= 1.1f)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, WaitTime);
                WaitTime += Time.deltaTime / time;
                yield return null;
            }
            
            canvasGroup.alpha = to;
            callBack?.Invoke();
        }
        
        //TODO: Convert to use material property block if/when possible
        public static IEnumerator Fade(this Material currentMaterial, int materialPropertyId, float from, float to, float time, Action callBack = null)
        {
            var WaitTime = 0.0f;
            while (WaitTime <= 1.1f)
            {
                currentMaterial.SetFloat(materialPropertyId, Mathf.Lerp(from, to, WaitTime));
                WaitTime += Time.deltaTime / time;
                yield return null;
            }
            currentMaterial.SetFloat(materialPropertyId, to);
            callBack?.Invoke();
        }

        public static IEnumerator Fade(this SpriteRenderer renderer, Color from, Color to, float time, Action callBack = null)
        {
            var WaitTime = 0.0f;
            while (WaitTime <= 1.1f)
            {
                renderer.color = Color.Lerp(from, to, WaitTime);
                WaitTime += Time.deltaTime / time;
                yield return null;
            }

            renderer.color = to;
            callBack?.Invoke();
        }

        public static IEnumerator Fade(this ClampedFloatParameter floatParameter, float from, float to, float time, Action callBack = null)
        {
            var WaitTime = 0.0f;
            while (WaitTime <= 1.1f)
            {
                floatParameter.value = Mathf.Lerp(from, to, WaitTime);
                WaitTime += Time.deltaTime / time;
                yield return null;
            }
        
            floatParameter.value = to;
            callBack?.Invoke();
        }

        public static IEnumerator DisableAfterSeconds(this GameObject currentGameObject, float seconds, Action callBack = null)
        {
            yield return new WaitForSeconds(seconds);
            currentGameObject.SetActive(false);
            callBack?.Invoke();
        }

        public static IEnumerator DisableAfterSeconds(this Behaviour currentBehaviour, float seconds, Action callBack = null)
        {
            yield return new WaitForSeconds(seconds);
            currentBehaviour.enabled = false;
            callBack?.Invoke();
        }

        public static IEnumerator FadeInText(this TextMeshProUGUI textMeshProText, string text,
            float delayBetweenConsecutiveTexts, float displayDuration, Action callBack = null)
        {
            textMeshProText.text = text;
            
            textMeshProText.CrossFadeAlpha(1, 0.95f, false);
            yield return new WaitForSeconds(displayDuration);
            callBack?.Invoke();
        }
        
        public static IEnumerator FadeOutText(this TextMeshProUGUI textMeshProText, string text,
            float delayBetweenConsecutiveTexts, Action callBack = null)
        {
            textMeshProText.text = text;
            
            textMeshProText.CrossFadeAlpha(0, 0.95f, false);
            yield return new WaitForSeconds(delayBetweenConsecutiveTexts);
            callBack?.Invoke();
        }
        
        public static IEnumerator ExecuteCallBackAfterSeconds(this Behaviour currentBehaviour, float seconds,
            Action callBack = null)
        {
            yield return new WaitForSeconds(seconds);
            callBack?.Invoke();
        }
        
        public static IEnumerator FadeText(this TextMeshProUGUI textMeshProText, float targetAlpha, float fadeDuration, float delayBeforeCallBack , Action callBack = null)
        {
            textMeshProText.CrossFadeAlpha(targetAlpha, fadeDuration, false);
            yield return new WaitForSeconds(delayBeforeCallBack);
            callBack?.Invoke();
        }

        public static IEnumerator Fade(this AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
        {
            var CurrentTime = 0.0f;
            audioMixer.GetFloat(exposedParam, out var CurrentVol);
            CurrentVol = Mathf.Pow(10, CurrentVol / 20);
            var TargetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (CurrentTime < duration)
            {
                CurrentTime += Time.deltaTime;
                var NewVol = Mathf.Lerp(CurrentVol, TargetValue, CurrentTime / duration);
                audioMixer.SetFloat(exposedParam, Mathf.Log10(NewVol) * 20);
                yield return null;
            }
        }
    }
}
