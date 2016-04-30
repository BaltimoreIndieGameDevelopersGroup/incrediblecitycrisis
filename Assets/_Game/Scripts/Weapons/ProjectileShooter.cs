using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component shoots projectiles when its parent's controller
    /// indicates a primary attack input. Add it to a child of the
    /// character, such as its hand.
    /// </summary>
    public class ProjectileShooter : MonoBehaviour
    {

        [Tooltip("Instantiate this prefab when firing.")]

        public GameObject projectilePrefab;

        [Tooltip("Instantiate the Projectile Prefab at this position.")]
        public Transform spawnPoint;

        [Tooltip("The force with which to launch the projectile.")]
        public float force = 500f;

        private VirtualController m_controller;
        private BasicCharacter2D m_character;

        protected virtual void Awake()
        {
            m_controller = GetComponentInParent<VirtualController>();
            m_character = GetComponentInParent<BasicCharacter2D>();
            if (m_controller == null) Debug.LogError("No VirtualController found on parent of " + name, this);
            if (m_character == null) Debug.LogError("No BasicCharacter2D found on parent of " + name, this);
            if (spawnPoint == null) spawnPoint = this.transform;
        }

        void Update()
        {
            if (m_controller.primaryAttack)
            {
                var projectileGameObject = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
                var projectile = projectileGameObject.GetComponent<Projectile>();
                var velocity = force * ((m_controller.move.magnitude > 0.1f) ? m_controller.move : new Vector2(m_character.facingLeft ? -1 : 1, 0));
                projectile.Fire(velocity);
            }
        }
    }
}