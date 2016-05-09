using UnityEngine.EventSystems;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This interface implements a Unity event handler for components that
    /// do something when a character's health has changed value.
    /// See: http://docs.unity3d.com/Manual/MessagingSystem.html
    /// </summary>
    public interface IHealthEventHandler : IEventSystemHandler
    {

        void HealthChanged(float normalizedHealth);

    }

}