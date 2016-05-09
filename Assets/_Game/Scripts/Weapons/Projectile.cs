using UnityEngine;
using UnityEngine.Events;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This is a simple projectile component. Add it to projectile GameObjects.
    /// It despawns the GameObject after a duration has passed or when it collides
    /// with something.
    /// </summary>
    public class Projectile : MonoBehaviour
    {

        [Tooltip("Duration in seconds.")]
        public float duration = 1;

        public UnityEvent onFire = new UnityEvent();

        public GameObjectEvent onCollision = new GameObjectEvent();

        public void Fire(Vector2 velocity)
        {
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
            Debug.Log(name + " collided with " + coll.collider.name);
            Despawn();
            onCollision.Invoke(coll.gameObject);
        }

    }
}