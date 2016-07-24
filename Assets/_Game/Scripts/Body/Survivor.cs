using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    public class Survivor : MonoBehaviour
    {

        public Hero hero;

        private Rigidbody2D rb;

        IEnumerator Start()
        {
            yield return null; // Give Hero a chance to spawn first.
            rb = GetComponent<Rigidbody2D>();
            hero = FindObjectOfType<Hero>();
            if (hero != null) hero.AddSurvivor();
        }

        void Update()
        {
            if (hero == null) return;
            var direction = new Vector2(hero.transform.position.x - transform.position.x, hero.transform.position.y - transform.position.y);
            rb.AddForce(direction);
        }

        public void OnDie(int killedByPlayerNumber)
        {
            GetComponent<Collider2D>().enabled = false;
            hero.RemoveSurvivor(killedByPlayerNumber);
            Destroy(this.gameObject);
        }
    }
}