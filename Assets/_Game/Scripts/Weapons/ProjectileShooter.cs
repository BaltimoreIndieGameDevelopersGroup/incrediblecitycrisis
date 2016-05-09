using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This attachment shoots projectiles it's attached to a character.
    /// </summary>
    public class ProjectileShooter : Attachment
    {

        [Tooltip("Instantiate this prefab when firing.")]

        public GameObject projectilePrefab;

        [Tooltip("Instantiate the Projectile Prefab at this position.")]
        public Transform spawnPoint;

        [Tooltip("The force with which to launch the projectile.")]
        public float force = 500f;

        private VirtualInput m_input;
        private Movement m_movement;

        public void OnAttachPlayer(Player player)
        {
            m_input = player.virtualInput;
            m_movement = GetComponentInParent<Movement>();
        }

        public void OnDetachPlayer()
        {
            m_input = null;
            m_movement = null;
        }

        protected virtual void Awake()
        {
            m_input = GetComponentInParent<VirtualInput>();
            m_movement = GetComponentInParent<Movement>();
            if (spawnPoint == null) spawnPoint = this.transform;
        }

        void Update()
        {
            if (m_input != null)
            {
                if (m_input.primaryAttack)
                {
                    var projectileGameObject = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
                    var projectile = projectileGameObject.GetComponent<Projectile>();
                    var facingLeft = (m_movement != null) ? m_movement.facingLeft : false;
                    var velocity = force * ((m_input.move.magnitude > 0.1f) ? m_input.move : new Vector2(facingLeft ? -1 : 1, 0));
                    projectile.Fire(velocity);
                }
            }
        }
    }
}