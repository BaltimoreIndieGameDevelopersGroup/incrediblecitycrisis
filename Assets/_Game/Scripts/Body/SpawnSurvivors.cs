using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    public class SpawnSurvivors : MonoBehaviour
    {

        public GameObject survivorPrefab;
        public int amount = 5;

        void Start()
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(survivorPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}