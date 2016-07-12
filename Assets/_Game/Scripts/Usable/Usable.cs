using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Abstract class for Usables, which are objects that a Body can interact with.
    /// It has an abstract method UseBy(user). Subclasses should override this method to 
    /// do something when the Body uses it.
    /// </summary>
    public abstract class Usable : MonoBehaviour
    {

        [Tooltip("Only usable by GameObjects with this tag.")]
        public string requiredTag = Tags.Hero;

        /// <summary>
        /// Checks if a character's tag will allow it to use this Usable.
        /// </summary>
        /// <param name="user">The character to check.</param>
        /// <returns>true if the character can use it; false otherwise.</returns>
        public bool UsableBy(GameObject user)
        {
            return (user != null) && string.Equals(user.tag, requiredTag);
        }

        /// <summary>
        /// Called when a character uses this Usable. This is an abstract method
        /// that subclasses should implement.
        /// </summary>
        /// <param name="user">The GameObject using the Usable.</param>
        public abstract void UseBy(GameObject user);

    }
}