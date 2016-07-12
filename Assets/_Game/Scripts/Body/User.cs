using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Allows the GameObject to use Usables.
    /// </summary>
    public class User : MonoBehaviour
    {

        // Records the Usable that the GameObject is currently standing on:
        private Usable m_currentUsable = null;

        private VirtualInput m_input = null;

        public virtual void OnAttachPlayer(Player player)
        {
            m_input = player.virtualInput;
            m_currentUsable = null;
        }

        public void OnDetachPlayer()
        {
            m_input = GetComponent<VirtualInput>();
        }

        /// <summary>
        /// When a GameObject enters the trigger collider of another GameObject,
        /// Unity invokes OnTriggerEnter2D on both GameObjects. This method checks
        /// the required tag and calls the UseBy method.
        /// </summary>
        /// <param name="other">The other GameObject involved in the trigger collision.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Usable))
            {
                var usable = other.GetComponent<Usable>();
                if ((usable != null) && usable.UsableBy(this.gameObject))
                {
                    m_currentUsable = usable;
                }
            }
        }

        private void Update() // Runs every frame.
        {
            if ((m_input != null) && m_input.useDown && (m_currentUsable != null))
            {
                // Player hit Use input, so use the Usable:
                m_currentUsable.UseBy(this.gameObject);
                m_currentUsable = null;
            }
        }

    }
}