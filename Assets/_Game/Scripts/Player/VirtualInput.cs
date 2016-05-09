using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component isolates input devices from character logic. Character logic code can read 
    /// inputs from virtual input without having to worry about what kind of input device
    /// is actually providing the inputs.
    /// </summary>
    public class VirtualInput : MonoBehaviour
    {

        [Tooltip("The amount in the X and Y directions that the input controller wants the character to move.")]
        public Vector2 move = Vector2.zero;

        [Tooltip("Initiate the primary attack this frame.")]
        public bool primaryAttack = false;

    }
}