using UnityEngine;

namespace BIG
{

    /// <summary>
    /// This component sets a VirtualController with inputs from a real player input device.
    /// </summary>
    [RequireComponent(typeof(VirtualController))]
    public class PlayerController : MonoBehaviour
    {

        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string primaryFire = "Fire1";

        private VirtualController m_virtualController;

        void Awake()
        {
            m_virtualController = GetComponent<VirtualController>();
            if (m_virtualController == null) Debug.LogError("No VirtualController found on " + name, this);
        }

        void Update()
        {
            m_virtualController.move = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            m_virtualController.primaryAttack = Input.GetButtonDown(primaryFire);
        }
    }
}