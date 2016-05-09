using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component sets a VirtualInput component with inputs from a real player input device.
    /// </summary>
    [RequireComponent(typeof(VirtualInput))]
    public class PlayerInput : MonoBehaviour
    {

        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string primaryFire = "Fire1";

        private VirtualInput m_virtualInput;

        void Awake()
        {
            m_virtualInput = GetComponent<VirtualInput>();
            if (m_virtualInput == null) Debug.LogError("No VirtualInput found on " + name, this);
        }

        void Update()
        {
            m_virtualInput.move = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            m_virtualInput.primaryAttack = Input.GetButtonDown(primaryFire);
        }
    }
}