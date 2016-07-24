using UnityEngine;

namespace BIG.IncredibleCityCrisis
{


    /// <summary>
    /// Manages data about a player's input and display.
    /// Spawns a body.
    /// </summary>
    public class Player : MonoBehaviour
    {

        [Tooltip("This player's player number.")]
        public int playerNumber;

        public Body heroPrefab;
        public Body shooterEnemyPrefab;

        public Body body { get; private set; }

        private void Start()
        {
            if (playerNumber == GameManager.heroPlayerNumber)
            {
                SpawnBody(heroPrefab);
                GetComponent<PlayerInput>().enabled = true;
            }
            else
            {
                SpawnBody(shooterEnemyPrefab);
            }
        }

        public void SpawnBody(Body prefab)
        {
            var spawnPosition = GetSpawnPosition(prefab);
            if (body != null)
            {
                BroadcastPlayerBodyConnection(Messages.OnDetachPlayer, body);
                Destroy(body.gameObject);
                body = null;
            }
            body = Instantiate(prefab, spawnPosition, Quaternion.identity) as Body;
            body.name = prefab.name + " Player " + playerNumber;
            BroadcastPlayerBodyConnection(Messages.OnAttachPlayer, body);
        }

        private void BroadcastPlayerBodyConnection(string message, Body body)
        {
            var playerBodyConnection = new PlayerBodyConnection(this, body);
            BroadcastMessage(message, playerBodyConnection);
            body.BroadcastMessage(message, playerBodyConnection);
        }

        private Vector3 GetSpawnPosition(Body prefab)
        {
            if (body != null)
            {
                // Spawn at our current (old) body's position:
                return body.transform.position;
            }
            else
            {
                // If no current body, spawn at the start position:
                var startPositions = FindObjectOfType<StartPositions>();
                return prefab.CompareTag(Tags.Hero) ? startPositions.heroPosition.position
                    : startPositions.EnemyPosition(playerNumber);
            }
        }

        public void BodyDied(int killerPlayerNumber)
        {
            Debug.Log(name + " body (" + body + ") was killed by player " + killerPlayerNumber);
            if (body.CompareTag(Tags.Hero))
            {
                PromoteNewHero(killerPlayerNumber);
            }
        }

        private void PromoteNewHero(int killerPlayerNumber)
        {
            foreach (var otherPlayer in FindObjectsOfType<Player>())
            {
                // Choose another player that's being played by a human:
                if (otherPlayer != this && otherPlayer.GetComponent<PlayerInput>().enabled) //if (otherPlayer.playerNumber == killerPlayerNumber)
                {
                    otherPlayer.SpawnBody(otherPlayer.heroPrefab);
                    SpawnBody(shooterEnemyPrefab);
                    return;
                }
            }
            SpawnBody(heroPrefab);
        }

    }

}