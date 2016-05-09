using UnityEngine.EventSystems;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This interface implements a Unity event handler for components that can die.
    /// The Die event is usually raised by a Health component.
    /// See: http://docs.unity3d.com/Manual/MessagingSystem.html
    /// </summary>
    public interface IDeathEventHandler : IEventSystemHandler
    {

        void Die();

    }

}