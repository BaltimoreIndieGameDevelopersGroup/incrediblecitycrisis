using UnityEngine;

namespace BIG.IncredibleCityCrisis
{


    /// <summary>
    /// Manages player input (controller) and output (HUD) and attaching bodies (characters) to players.
    /// </summary>
    public class Player : MonoBehaviour
    {

        public GameObject heroPrefab;

        public GameObject enemyPrefab;

        public GameObject body;

        public PlayerHUD hud { get; private set; }

        public VirtualInput virtualInput { get; private set; }

        public PlayerInput playerInput { get; private set; }

        private void Awake()
        {
            hud = GetComponent<PlayerHUD>();
            virtualInput = GetComponent<VirtualInput>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            PossessBody(body);
        }

        public void PossessBody(GameObject newBody)
        {
            if (body != null)
            {
                ReleaseBody();
            }
            if (newBody != null)
            {
                Debug.Log(name + " possessing body " + newBody);
                body = newBody;
                body.SetActive(true);
                body.BroadcastMessage("OnAttachPlayer", this);
            }
        }

        public void ReleaseBody()
        {
            if (body != null)
            {
                Debug.Log(name + " releasing body");
                var oldBody = body;
                body = null;
                oldBody.BroadcastMessage("OnDetachPlayer");
            }
        }

        public void SpawnBody(GameObject prefab)
        {
            var oldBody = body;
            var newBody = Instantiate(prefab, oldBody.transform.position, Quaternion.identity) as GameObject;
            PossessBody(newBody);
            Destroy(oldBody);
        }

        public void BecomeHero()
        {
            Debug.Log(name + " becoming hero");
            if (!body.CompareTag("Enemy"))
            {
                ReleaseBody();
            }
            SpawnBody(heroPrefab);
        }

        public void BecomeEnemy()
        {
            Debug.Log(name + " becoming enemy");
            SpawnBody(enemyPrefab);
        }



    }
}