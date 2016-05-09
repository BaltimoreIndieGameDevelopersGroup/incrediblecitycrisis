using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Hero body. When it dies, it promotes another player to be the hero.
    /// </summary>
    public class Hero : Body, IDeathEventHandler
    {

        public void Die()
        {
            var thisPlayer = player;
            foreach (var otherPlayer in FindObjectsOfType<Player>())
            {
                if (otherPlayer != thisPlayer && otherPlayer.body != null)
                {
                    otherPlayer.BecomeHero();
                    thisPlayer.BecomeEnemy();
                    return;
                }
            }
            thisPlayer.BecomeHero();
        }

    }
}