using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Sets a Body's VirtualInput component with inputs from a physical input device.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {

        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string use = "Fire2";
        public string primaryAttack = "Fire1";

        private VirtualInput m_virtualInput;

        public void OnAttachPlayer(PlayerBodyConnection connection)
        {
            m_virtualInput = (connection.body != null) ? connection.body.GetComponent<VirtualInput>() : null;
        }

        public void OnDetachPlayer(PlayerBodyConnection connection)
        {
            m_virtualInput = null;
        }

        void Update()
        {
            if (m_virtualInput != null)
            {
                m_virtualInput.move = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
                m_virtualInput.useDown = Input.GetButtonDown(use);
                m_virtualInput.primaryAttackDown = Input.GetButtonDown(primaryAttack);
                m_virtualInput.primaryAttackHeld = Input.GetButton(primaryAttack);
            }
        }
    }
}