using UnityEngine.EventSystems;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This interface implements a Unity event handler for components that take damage,
    /// such as Health.cs.
    /// See: http://docs.unity3d.com/Manual/MessagingSystem.html
    /// </summary>
    public interface IDamageEventHandler : IEventSystemHandler
    {

        void TakeDamage(float health);

    }

}