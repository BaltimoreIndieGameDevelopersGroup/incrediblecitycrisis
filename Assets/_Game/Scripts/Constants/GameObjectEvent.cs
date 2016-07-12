using UnityEngine;
using UnityEngine.Events;
using System;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// UnityEvent with a GameObject parameter.
    /// https://docs.unity3d.com/Manual/UnityEvents.html
    /// </summary>
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

}