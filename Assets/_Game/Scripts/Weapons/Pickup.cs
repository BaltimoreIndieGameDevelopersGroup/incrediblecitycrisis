using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This is the abstract class for pickups.
    /// </summary>
    public abstract class Pickup : MonoBehaviour
    {

        public string requiredTag = "Hero";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!string.IsNullOrEmpty(requiredTag) && other.CompareTag(requiredTag))
            {
                PickUpBy(other.gameObject);
            }
        }

        public abstract void PickUpBy(GameObject user);

    }
}