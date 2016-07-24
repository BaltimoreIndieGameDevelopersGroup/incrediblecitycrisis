using UnityEngine;
using UnityEngine.Events;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Maintains a health value, sending messages & events when it changes or drops to zero.
    /// </summary>
    public class Health : MonoBehaviour
    {

        public float maxHealth = 100f;

        public float currentHealth = 100f;

        public FloatEvent onHealthChange = new FloatEvent();

        public UnityEvent onDie = new UnityEvent();

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (currentHealth > 0)
            {
                Debug.Log(name + " taking " + damageInfo.damage + " damage from player " + damageInfo.fromPlayerNumber);
                var newHealth = currentHealth - damageInfo.damage;
                currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
                var normalizedHealth = currentHealth / maxHealth;
                onHealthChange.Invoke(normalizedHealth);
                BroadcastMessage(Messages.OnHealthChange, normalizedHealth, SendMessageOptions.DontRequireReceiver);
                if (currentHealth <= 0)
                {
                    onDie.Invoke();
                    BroadcastMessage(Messages.OnDie, damageInfo.fromPlayerNumber, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}