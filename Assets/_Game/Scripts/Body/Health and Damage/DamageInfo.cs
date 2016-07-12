using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Bundles damage amount and the source of the damage, since we need to 
    /// know who delivered killing blows to promote that player to be the
    /// new hero.
    /// </summary>
    public struct DamageInfo
    {

        public float damage;

        public int fromPlayerNumber;

        public DamageInfo(float damage, int fromPlayerNumber = 0)
        {
            this.damage = damage;
            this.fromPlayerNumber = fromPlayerNumber;
        }

    }
}