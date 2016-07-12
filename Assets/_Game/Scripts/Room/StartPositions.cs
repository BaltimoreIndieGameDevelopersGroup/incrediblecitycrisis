using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Specifies starting positions when starting the current level.
    /// </summary>
    public class StartPositions : MonoBehaviour
    {

        public Transform heroPosition;
        public Transform enemy1Position;
        public Transform enemy2Position;
        public Transform enemy3Position;
        public Transform enemy4Position;

        public Vector3 EnemyPosition(int playerNumber)
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
                    Debug.LogError("StartPositions.EnemyPosition: playerNumber must be in the range 1-4.");
                    return Vector3.zero;
            }
        }

        // Unity invokes this method in the Scene view. Draw info markers for the level designer:
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (heroPosition != null) Gizmos.DrawWireSphere(heroPosition.transform.position, 0.5f);
            Gizmos.color = Color.red;
            if (enemy1Position != null) Gizmos.DrawWireSphere(enemy1Position.transform.position, 0.5f);
            if (enemy2Position != null) Gizmos.DrawWireSphere(enemy2Position.transform.position, 0.5f);
            if (enemy3Position != null) Gizmos.DrawWireSphere(enemy3Position.transform.position, 0.5f);
            if (enemy4Position != null) Gizmos.DrawWireSphere(enemy4Position.transform.position, 0.5f);
        }

    }
}