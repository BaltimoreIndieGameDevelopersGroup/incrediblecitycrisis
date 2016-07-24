using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// A Weapon that shoots Bullets.
    /// </summary>
    public class Gun : Weapon
    {

        [Tooltip("Instantiate this prefab when firing.")]
        public Bullet bulletPrefab;

        [Tooltip("Instantiate the Bullet Prefab at this position.")]
        public Transform spawnPoint;

        [Tooltip("The force with which to launch the projectile.")]
        public float force = 500f;

        protected override void Awake()
        {
            if (spawnPoint == null) spawnPoint = this.transform;
        }

        public override void Fire()
        {
            base.Fire();
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation) as Bullet;
            var velocity = force * fireDirection;
            var playerNumber = 0; //[TODO]? (m_player != null) ? m_player.playerNumber : 0;
            bullet.Fire(velocity, playerNumber);
        }

    }
}