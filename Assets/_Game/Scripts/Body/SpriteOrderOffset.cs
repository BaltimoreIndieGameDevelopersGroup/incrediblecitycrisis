using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Works with SpriteOrderByYPosition to add a fine-tuning offset to the sortingOrder.
    /// This allows some child SpriteRenderers to be rendered on top of others so the 
    /// layering looks correct.
    /// </summary>
    public class SpriteOrderOffset : MonoBehaviour
    {

        // Offset to add to the sortingOrder computed by SpriteOrderByYPosition:
        public int offset;

    }

}