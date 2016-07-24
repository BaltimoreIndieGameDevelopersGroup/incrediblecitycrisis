using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component is a stand-in AI. It randomly sets the primary attack input 
    /// at a regular interval. If the player's physical fire button is pressed, it turns 
    /// off the AI and makes the player possess the character.
    /// </summary>
    public class EnemyAI : MonoBehaviour
    {

        [Tooltip("Do the primary attack at this frequency in seconds.")]
        public float fireRate = 5;

        private VirtualInput m_input;

        private void Awake()
        {
            m_input = GetComponent<VirtualInput>();
        }

        private IEnumerator Start()
        {
            // Run a continuous coroutine that sets the controller's primary attack
            // input true when the fire rate duration has passed.
            yield return new WaitForSeconds(Random.value * fireRate); // (stagger enemies' fire)
            while (true)
            {
                yield return new WaitForSeconds(fireRate);
                m_input.move = new Vector2(2 * Random.value - 1, 2 * Random.value - 1);
                m_input.primaryAttackDown = true;
                m_input.primaryAttackHeld = true;
                yield return null; // Wait 2 frames. The first frame starts the weapon power-up.
                yield return null; // The second frame actually fires.
                m_input.move = Vector2.zero;
                m_input.primaryAttackDown = false;
                m_input.primaryAttackHeld = false;
            }
        }

    }
}