using UnityEngine;
using System.Collections;

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

        public UnityEngine.Events.UnityEvent onCollision = new UnityEngine.Events.UnityEvent();

        public void Fire(Vector2 velocity)
        {
            var rb2d = GetComponent<Rigidbody2D>();
            if (rb2d != null) rb2d.AddForce(velocity);
            Invoke("Despawn", duration);
        }

        protected virtual void Despawn()
        {
            Destroy(this.gameObject);
        }

        protected virtual void OnCollisionEnter2D(Collision2D coll)
        {
            Debug.Log("Collision with: " + coll.collider.name);
            Despawn();
            onCollision.Invoke();
        }

    }
}