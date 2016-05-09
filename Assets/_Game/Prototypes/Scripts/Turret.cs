using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component is a prototype turret. Enemies can enter it and fire.
    /// The turret automatically ejects the player after a specified duration.
    /// </summary>
    public class Turret : Body
    {

        public string requiredTag = "Enemy";

        public float automaticEjectTime = 5;

        public bool m_isAvailable = true;
        private GameObject m_user = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_isAvailable && !string.IsNullOrEmpty(requiredTag) && other.CompareTag(requiredTag))
            {
                var body = other.GetComponent<Body>();
                if (body != null && body.player != null)
                {
                    m_isAvailable = false;
                    body.player.PossessBody(this.gameObject);
                    m_user = body.gameObject;
                    body.gameObject.SetActive(false);
                    Invoke("Eject", automaticEjectTime);
                }
            }
        }

        public override void OnDetachPlayer()
        {
            Eject();
            Invoke("BecomeAvailable", 2);
            base.OnDetachPlayer();
        }

        public void Eject()
        {
            if (player != null) player.PossessBody(m_user);

        }

        private void BecomeAvailable()
        {
            m_isAvailable = true;
        }

    }
}