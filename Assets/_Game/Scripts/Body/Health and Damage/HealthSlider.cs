using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Shows a healthbar.
    /// </summary>
    public class HealthSlider : MonoBehaviour
    {

        [Tooltip("When health changes, show the slider at full opacity for this duration.")]
        public float showSeconds = 1;

        [Tooltip("After showing at full opacity, fade out the slider over this duration.")]
        public float fadeSeconds = 2;

        private UnityEngine.UI.Slider m_slider;
        private CanvasGroup m_canvasGroup;

        private void Start()
        {
            m_slider = GetComponent<UnityEngine.UI.Slider>();
            if (m_slider == null) Debug.LogError(name + " requires a Slider", this);
            m_canvasGroup = GetComponent<CanvasGroup>();
            if (m_canvasGroup == null) Debug.LogError(name + " requires a Canvas Group", this);
            m_canvasGroup.alpha = 0;
        }

        public void OnHealthChange(float normalizedHealth)
        {
            m_slider.value = normalizedHealth;
            StopAllCoroutines();
            StartCoroutine(ShowSliderWithFade());
        }

        public IEnumerator ShowSliderWithFade()
        {
            m_canvasGroup.alpha = 1;
            yield return new WaitForSeconds(showSeconds);
            float remaining = fadeSeconds;
            while (remaining > 0)
            {
                yield return null;
                remaining -= Time.deltaTime;
                m_canvasGroup.alpha = Mathf.Clamp(remaining / 2, 0, 1);
            }
            m_canvasGroup.alpha = 0;
        }

    }
}
