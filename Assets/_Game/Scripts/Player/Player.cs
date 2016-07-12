using UnityEngine;

namespace BIG.IncredibleCityCrisis
{


    /// <summary>
    /// Manages data about a player's input, display, and current Body.
    /// The player's body can change between the hero, enemy, and other 
    /// bodies that the enemy can possess or change into.
    /// </summary>
    public class Player : MonoBehaviour
    {

        [Tooltip("This player's player number.")]
        public int playerNumber;

        [Tooltip("This player's hero prefab.")]
        public Body heroPrefab;

        [Tooltip("This player's enemy prefab.")]
        public Body enemyPrefab;

        public VirtualInput virtualInput { get; private set; }

        public PlayerInput playerInput { get; private set; }

        public PlayerDisplay display { get; private set; }

        public Body currentBody { get; private set; }

        private void Awake()
        {
            virtualInput = GetComponent<VirtualInput>();
            playerInput = GetComponent<PlayerInput>();
            display = GetComponent<PlayerDisplay>();
        }

        private void Start()
        {
            if (playerNumber == GameManager.heroPlayerNumber)
            {
                BecomeHero();
            }
            else
            {
                BecomeEnemy();
            }
        }

        public void BecomeHero()
        {
            Debug.Log(name + " becoming hero");
            ReleaseTemporaryBody();
            SpawnBody(heroPrefab);
        }

        public void BecomeEnemy()
        {
            Debug.Log(name + " becoming enemy");
            SpawnBody(enemyPrefab);
        }

        public void SpawnBody(Body prefab)
        {
            var spawnPosition = GetSpawnPosition(prefab);
            if (currentBody != null)
            {
                Destroy(currentBody.gameObject);
                currentBody = null;
            }
            var newBody = Instantiate(prefab, spawnPosition, Quaternion.identity) as Body;
            newBody.name = prefab.name + " Player " + playerNumber;
            PossessBody(newBody);
        }

        private Vector3 GetSpawnPosition(Body prefab)
        {
            if (currentBody != null)
            {
                // Spawn at our current (old) body's position:
                return currentBody.transform.position;
            }
            else
            {
                // If no current body, spawn at the start position:
                var startPositions = FindObjectOfType<StartPositions>();
                return prefab.CompareTag(Tags.Hero) ? startPositions.heroPosition.position
                    : startPositions.EnemyPosition(playerNumber);
            }
        }

        public void PossessBody(Body newBody)
        {
            if (currentBody != null)
            {
                ReleaseBody();
            }
            if (newBody != null)
            {
                Debug.Log(name + " possessing currentBody " + newBody);
                currentBody = newBody;
                currentBody.gameObject.SetActive(true);
                currentBody.BroadcastMessage(Messages.OnAttachPlayer, this);
            }
        }

        public void ReleaseBody()
        {
            if (currentBody != null)
            {
                Debug.Log(name + " releasing currentBody");
                var oldBody = currentBody;
                currentBody = null;
                oldBody.BroadcastMessage(Messages.OnDetachPlayer);
            }
        }

        public void ReleaseTemporaryBody()
        {
            if (currentBody != null && !currentBody.CompareTag(Tags.Hero) && !currentBody.CompareTag(Tags.Enemy))
            {
                ReleaseBody();
                if (currentBody == null) BecomeEnemy();
            }
        }

        public void BodyDied(int killerPlayerNumber)
        {
            Debug.Log(name + " body (" + currentBody + ") was killed by player " + killerPlayerNumber);
            if (currentBody.CompareTag(Tags.Hero))
            {
                PromoteNewHero(killerPlayerNumber);
            }
            else
            {
                BecomeEnemy();
            }
        }

        private void PromoteNewHero(int killerPlayerNumber)
        {
            foreach (var otherPlayer in FindObjectsOfType<Player>())
            {
                if (otherPlayer.playerNumber == killerPlayerNumber)
                {
                    otherPlayer.BecomeHero();
                    BecomeEnemy();
                    return;
                }
            }
            BecomeHero();
        }

    }
}
