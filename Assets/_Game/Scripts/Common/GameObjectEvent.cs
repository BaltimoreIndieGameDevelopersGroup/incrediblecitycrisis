using UnityEngine;
using UnityEngine.Events;
using System;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// The class defines a Unity event with a GameObject parameter.
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

}