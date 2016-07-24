using UnityEngine;
using System.Collections;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Transitions gameplay to another area. When the hero enters the trigger collider,
    /// it moves the camera and all players to new positions.
    /// </summary>
    public class RoomExit : MonoBehaviour
    {

        public Transform cameraPosition;
        public Transform heroPosition;
        public Transform enemy1Position;
        public Transform enemy2Position;
        public Transform enemy3Position;
        public Transform enemy4Position;

        public float teleportSeconds = 0.5f;

        private static float timeNextExitAllowed = 0;

        private IEnumerator OnTriggerEnter2D(Collider2D other)
        {
            if ((Time.time >= timeNextExitAllowed) && other.CompareTag(Tags.Survivor))
            {
                // Prevent hopping between rooms too quickly:
                timeNextExitAllowed = Time.time + 1f;

                // Teleport to the destination.

                // Make all enemies regular (leave turrets, mechs, etc.) and 
                // disable player body colliders so they don't trigger anything while teleporting:
                var players = FindObjectsOfType<Player>();
                foreach (var player in players)
                {
                    //player.ReleaseTemporaryBody();
                    var collider2D = player.body.GetComponent<Collider2D>();
                    if (collider2D != null) collider2D.enabled = false;
                }

                // Record original positions so we can compute intermediate positions:
                var originalCameraPosition = Camera.main.transform.position;
                var originalHeroPosition = other.transform.position;
                var originalPlayerPositions = new Vector3[players.Length];
                for (int i = 0; i < players.Length; i++)
                {
                    originalPlayerPositions[i] = players[i].body.transform.position;
                }

                // Teleport hero center of mass:
                FindObjectOfType<Hero>().transform.position = heroPosition.position;

                // Lerp to new positions:
                var survivors = GameObject.FindObjectsOfType<Survivor>();
                float elapsed = 0;
                while (elapsed < teleportSeconds)
                {
                    var t = elapsed / teleportSeconds;
                    Camera.main.transform.position = Vector3.Lerp(originalCameraPosition, cameraPosition.position, t);
                    //other.transform.position = Vector3.Lerp(originalHeroPosition, heroPosition.position, t);
                    for (int i = 0; i < survivors.Length; i++)
                    {
                        survivors[i].transform.position = Vector3.Lerp(originalHeroPosition, heroPosition.position, t);
                    }
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i].body.CompareTag(Tags.Enemy))
                        {
                            players[i].body.transform.position = Vector3.Lerp(originalPlayerPositions[i], GetEnemyPosition(players[i].playerNumber), t);
                        }
                    }
                    yield return null;
                    elapsed += Time.deltaTime;
                }

                // Re-enable player body colliders:
                foreach (var player in players)
                {
                    if (!player.body.CompareTag(Tags.Hero)) // Hero doesn't use a collider.
                    {
                        var collider2D = player.body.GetComponent<Collider2D>();
                        if (collider2D != null) collider2D.enabled = true;
                    }
                }
            }
        }

        private Vector3 GetEnemyPosition(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1:
                    return enemy1Position.position;
                case 2:
                    return enemy2Position.position;
                case 3:
                    return enemy3Position.position;
                case 4:
                    return enemy4Position.position;
                default:
                    Debug.LogError("RoomExit.GetEnemyPosition received invalid playerNumber " + playerNumber);
                    return Vector3.zero;
            }
        }

        // Unity invokes the methods below in the Scene view. They draw info markers for the level designer:

        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "RoomExit.png");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            if (cameraPosition != null) Gizmos.DrawWireSphere(cameraPosition.position, 0.5f);
            Gizmos.color = Color.green;
            if (heroPosition != null) Gizmos.DrawWireSphere(heroPosition.position, 0.5f);
            Gizmos.color = Color.red;
            if (enemy1Position!= null) Gizmos.DrawWireSphere(enemy1Position.position, 0.5f);
            if (enemy2Position != null) Gizmos.DrawWireSphere(enemy2Position.position, 0.5f);
            if (enemy3Position != null) Gizmos.DrawWireSphere(enemy3Position.position, 0.5f);
            if (enemy4Position != null) Gizmos.DrawWireSphere(enemy4Position.position, 0.5f);
        }

    }
}