using UnityEngine.Events;
using System;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// UnityEvent with a float parameter.
    /// https://docs.unity3d.com/Manual/UnityEvents.html
    /// </summary>
    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

}