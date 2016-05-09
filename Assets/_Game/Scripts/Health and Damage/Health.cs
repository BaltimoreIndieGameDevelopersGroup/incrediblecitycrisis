using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component tracks a health value.
    /// </summary>
    public class Health : MonoBehaviour, IDamageEventHandler
    {

        public float maxHealth = 100f;

        public float currentHealth = 100f;

        public FloatEvent onHealthChange = new FloatEvent();

        public UnityEvent onDie = new UnityEvent();

        private GameObject notificationTarget = null;

        public float normalizedHealth
        {
            get { return currentHealth / maxHealth; }
        }

        public void OnAttachPlayer(Player player)
        {
            notificationTarget = player.gameObject;
            NotifyHealthChanged();
        }

        public void OnDetachPlayer()
        {
            notificationTarget = null;
        }

        public void Start()
        {
            NotifyHealthChanged();
        }

        private void NotifyHealthChanged()
        {
            if (notificationTarget != null && notificationTarget != this.gameObject)
            {
                ExecuteEvents.Execute<IHealthEventHandler>(notificationTarget, null, (x, y) => x.HealthChanged(normalizedHealth));
            }
            ExecuteEvents.Execute<IHealthEventHandler>(this.gameObject, null, (x, y) => x.HealthChanged(normalizedHealth));
            onHealthChange.Invoke(normalizedHealth);
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            NotifyHealthChanged();
            if (currentHealth <= 0)
            {
                NotifyDied();
            }
        }

        private void NotifyDied()
        {
            if (notificationTarget != null && notificationTarget != this.gameObject)
            {
                ExecuteEvents.Execute<IDeathEventHandler>(notificationTarget, null, (x, y) => x.Die());
            }
            ExecuteEvents.Execute<IDeathEventHandler>(this.gameObject, null, (x, y) => x.Die());
            onDie.Invoke();
        }
    }
}