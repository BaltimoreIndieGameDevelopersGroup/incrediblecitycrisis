using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Isolates input devices from character logic. Character logic code can read inputs from 
    /// VirtualInput having to worry about what kind of device (physical player input device or 
    /// AI control) is actually providing the inputs. 
    /// </summary>
    public class VirtualInput : MonoBehaviour
    {

        [Tooltip("The amount in the X and Y directions that the input controller wants the character to move.")]
        public Vector2 move = Vector2.zero;

        [Tooltip("Use the item that the character is currently standing on.")]
        public bool useDown = false;

        [Tooltip("Initiate the primary attack this frame.")]
        public bool primaryAttackDown = false;

        [Tooltip("Primary attack input is being held down.")]
        public bool primaryAttackHeld = false;

    }
}