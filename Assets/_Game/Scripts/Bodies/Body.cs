using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component keeps track of which player is attached to the character.
    /// </summary>
    public class Body : MonoBehaviour
    {

        public Player player;

        public virtual void OnAttachPlayer(Player newPlayer)
        {
            player = newPlayer;
        }

        public virtual void OnDetachPlayer()
        {
            player = null;
            var rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

    }
}