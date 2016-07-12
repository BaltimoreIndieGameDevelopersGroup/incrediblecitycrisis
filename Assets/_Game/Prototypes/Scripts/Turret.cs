using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Prototype turret. Enemies can enter it and fire, or press the Use button
    /// to exit.
    /// </summary>
    public class Turret : Body
    {

        public string requiredTag = Tags.Enemy;

        public bool automaticEject = false;

        public float automaticEjectTime = 5;

        private bool m_isAvailable = true;

        private Body m_user = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_isAvailable && !string.IsNullOrEmpty(requiredTag) && other.CompareTag(requiredTag))
            {
                var body = other.GetComponent<Body>();
                if (body != null && body.player != null)
                {
                    m_isAvailable = false;
                    body.player.PossessBody(this);
                    m_user = body;
                    body.gameObject.SetActive(false);
                    if (automaticEject) Invoke("Eject", automaticEjectTime);
                }
            }
        }

        private void Update()
        {
            if (player != null && player.virtualInput.useDown)
            {
                Eject();
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