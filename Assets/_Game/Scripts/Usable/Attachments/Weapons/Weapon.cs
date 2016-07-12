using UnityEngine;
using UnityEngine.Events;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// An Attachment that does something when the attack input is held down.
    /// </summary>
    public class Weapon : Attachment
    {

        [Tooltip("Player must hold down attack input for this duration before the attack fires.")]
        public float powerUpSeconds = 0;

        [Tooltip("Player must wait this duration after firing to fire again.")]
        public float powerDownSeconds = 1;

        public UnityEvent onPowerUp = new UnityEvent();
        public UnityEvent onFire = new UnityEvent();
        public UnityEvent onPowerDown = new UnityEvent();
        public UnityEvent onIdle = new UnityEvent();

        protected Player m_player;
        protected VirtualInput m_input;
        protected Movement m_movement;
        protected enum WeaponState { Idle, PoweringUp, PoweringDown }
        protected WeaponState m_weaponState;
        protected float m_secondsLeft;

        public virtual void OnAttachPlayer(Player player)
        {
            m_player = player;
            m_input = player.virtualInput;
            m_movement = GetComponentInParent<Movement>();
            m_weaponState = WeaponState.Idle;
        }

        public virtual void OnDetachPlayer()
        {
            m_player = null;
            m_input = null;
            m_movement = null;
        }

        void Update()
        {
            if (m_input == null) return;
            if (m_input.primaryAttackHeld)
            {
                switch (m_weaponState)
                {
                    case WeaponState.Idle:
                        StartPoweringUp();
                        break;
                    case WeaponState.PoweringUp:
                        m_secondsLeft -= Time.deltaTime;
                        if (m_secondsLeft <= 0) Fire();
                        break;
                    case WeaponState.PoweringDown:
                        m_secondsLeft -= Time.deltaTime;
                        if (m_secondsLeft <= 0) StartPoweringUp();
                        break;
                }
            }
            else if (m_weaponState != WeaponState.Idle)
            {
                StartIdling();
            }
        }

        public virtual void StartPoweringUp()
        {
            m_weaponState = WeaponState.PoweringUp;
            m_secondsLeft = powerUpSeconds;
            onPowerUp.Invoke();
        }

        public virtual void Fire()
        {
            onFire.Invoke();
            StartPoweringDown();
        }

        public virtual void StartPoweringDown()
        {
            m_weaponState = WeaponState.PoweringDown;
            m_secondsLeft = powerDownSeconds;
            onPowerDown.Invoke();
        }

        public virtual void StartIdling()
        {
            m_weaponState = WeaponState.Idle;
            onIdle.Invoke();
        }

        public Vector3 fireDirection
        {
            get // The direction of fire is the same as the direction of movement.
            {
                var facingLeft = (m_movement != null) ? m_movement.facingLeft : false;
                return ((m_input.move.magnitude > 0.1f) ? m_input.move : new Vector2(facingLeft ? -1 : 1, 0)).normalized;
            }
        }

    }
}
