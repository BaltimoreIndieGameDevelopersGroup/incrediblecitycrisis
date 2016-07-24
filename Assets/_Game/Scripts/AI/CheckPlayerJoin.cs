using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// If the player presses the attack button, disable enemy AI.
    /// </summary>
    public class CheckPlayerJoin : MonoBehaviour
    {

        private PlayerInput m_playerInput;

        public virtual void OnAttachPlayer(PlayerBodyConnection connection)
        {
            m_playerInput = connection.player.GetComponent<PlayerInput>();
        }

        void Update()
        {
            if (m_playerInput != null &&
                (Input.GetButton(m_playerInput.primaryAttack) ||
                Mathf.Abs(Input.GetAxisRaw(m_playerInput.horizontalAxis)) > 0.1f ||
                Mathf.Abs(Input.GetAxisRaw(m_playerInput.verticalAxis)) > 0.1f))
            {
                m_playerInput.enabled = true;
                Destroy(GetComponent<EnemyAI>());
                Destroy(this);
            }
        }
	}
}
