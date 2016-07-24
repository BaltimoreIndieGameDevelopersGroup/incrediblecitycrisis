using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Keeps track of which player is attached to the body. Also informs the player
    /// if the body dies.
    /// </summary>
    public class Hero : Body
    {

        public int numSurvivors;

        protected override void Awake()
        {
            base.Awake();
            numSurvivors = 0;
        }

        public void AddSurvivor()
        {
            numSurvivors++;
        }

        public void RemoveSurvivor(int killedByPlayerNumber)
        {
            numSurvivors--;
            if (numSurvivors <= 0) player.BodyDied(killedByPlayerNumber);
        }

    }
}