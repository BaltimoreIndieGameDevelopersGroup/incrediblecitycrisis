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

        // !!! NOTE: This script currently doesn't do anything !!!

        [Tooltip("Do the primary attack at this frequency in seconds.")]
        public float fireRate = 5;

        public Player player;

        private VirtualInput m_input;

        private string playerJoinButton { get { return player.playerInput.primaryAttack; } }

        private void Awake()
        {
            m_input = GetComponent<VirtualInput>();
        }

        private IEnumerator Start()
        {
            // Run a continuous coroutine that sets the controller's primary attack
            // input true for 1 frame when the fire rate duration has passed.
            yield return new WaitForSeconds(Random.value); // (stagger enemies' fire)
            while (true)
            {
                yield return new WaitForSeconds(fireRate);
                m_input.primaryAttackDown = true;
                m_input.primaryAttackHeld = true;
                yield return null;
                m_input.primaryAttackDown = false;
                m_input.primaryAttackHeld = false;
            }
        }

        private void Update()
        {
            // Check for a human player joining:
            if (!string.IsNullOrEmpty(playerJoinButton) && Input.GetButtonDown(playerJoinButton))
            {
                GrantPlayerControl();
            }
        }

        public void GrantPlayerControl()
        {
            Debug.Log("Granting player control to " + player, this);
            enabled = false; // Disable enemy AI.
            StopAllCoroutines(); // Stop the automatic fire coroutine.
            //player.PossessBody(this);
        }

    }
}