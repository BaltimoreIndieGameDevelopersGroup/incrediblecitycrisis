using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Keeps track of which player is attached to the body. Also informs the player
    /// if the body dies.
    /// </summary>
    public class Body : MonoBehaviour
    {

        public Player player { get; private set; }

        protected virtual void Awake()
        {
            player = null;
        }

        public virtual void OnAttachPlayer(PlayerBodyConnection connection)
        {
            player = connection.player;
        }

        public virtual void OnDetachPlayer(PlayerBodyConnection connection)
        {
            player = null;
            var rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        public virtual void OnDie(int fromPlayerNumber)
        {
            if (player != null) player.BodyDied(fromPlayerNumber);
        }

    }
}