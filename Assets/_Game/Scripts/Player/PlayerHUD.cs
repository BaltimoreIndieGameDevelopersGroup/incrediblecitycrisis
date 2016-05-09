using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Manages a player's HUD.
    /// </summary>
    public class PlayerHUD : MonoBehaviour, IHealthEventHandler
    {

        public UnityEngine.UI.Slider slider;

        public void HealthChanged(float normalizedHealth)
        {
            slider.value = normalizedHealth;
        }
    }
}