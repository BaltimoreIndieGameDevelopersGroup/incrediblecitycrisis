using UnityEngine;
using UnityEngine.EventSystems;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component provides the ability to deal damage to a target, if the target
    /// implements the IDamageEventHandler interface.
    /// </summary>
    public class DamageDealer : MonoBehaviour
    {

        [Tooltip("Amount of damage to deal.")]
        public float damage = 1;

        public void DealDamage(GameObject target)
        {
            ExecuteEvents.Execute<IDamageEventHandler>(target, null, (x, y) => x.TakeDamage(damage));
        }

    }
}