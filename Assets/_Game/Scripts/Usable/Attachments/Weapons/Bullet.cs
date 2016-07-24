using UnityEngine;
using UnityEngine.Events;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Simple projectile fired by a Gun. When fired, it launches with a specified velocity 
    /// until the duration has passed or it collides with something.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {

        public string[] hitsTags;

        public float minDamage = 10;

        public float maxDamage = 20;

        [Tooltip("Duration in seconds. If it doesn't hit anything before this duration, it despawns.")]
        public float duration = 1;

        public UnityEvent onFire = new UnityEvent();

        public GameObjectEvent onCollision = new GameObjectEvent();

        private int m_playerNumber;

        public void Fire(Vector2 velocity, int playerNumber)
        {
            m_playerNumber = playerNumber;
            var rb2d = GetComponent<Rigidbody2D>();
            if (rb2d != null) rb2d.AddForce(velocity);
            onFire.Invoke();
            Invoke("Despawn", duration);
        }

        protected virtual void Despawn()
        {
            Destroy(this.gameObject);
        }

        protected virtual void OnCollisionEnter2D(Collision2D coll)
        {
            for (int i = 0; i < hitsTags.Length; i++)
            {
                if (string.Equals(hitsTags[i], coll.gameObject.tag))
                {
                    Hit(coll.gameObject);
                }
            }
            Hit(coll.gameObject);
        }

        protected virtual void Hit(GameObject target)
        { 
            Debug.Log(name + " collided with " + target.name);
            Despawn();
            var damage = Random.Range(minDamage, maxDamage);
            target.SendMessageUpwards(Messages.OnTakeDamage, new DamageInfo(damage, m_playerNumber), SendMessageOptions.DontRequireReceiver);
            onCollision.Invoke(target);
        }

    }
}
