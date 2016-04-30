using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component sets the character's primary attack input at a
    /// regular interval.
    /// </summary>
    public class Turret : MonoBehaviour
    {

        [Tooltip("Do the primary attack at this frequency in seconds.")]
        public float fireRate = 5;

        private VirtualController m_controller;

        private void Awake()
        {
            m_controller = GetComponent<VirtualController>();
        }

        private IEnumerator Start()
        {
            // Run a continuous coroutine that sets the controller's primary attack
            // input true for 1 frame when the fire rate duration has passed.
            while (true)
            {
                yield return new WaitForSeconds(fireRate);
                m_controller.primaryAttack = true;
                yield return null;
                m_controller.primaryAttack = false;
            }
        }

    }
}