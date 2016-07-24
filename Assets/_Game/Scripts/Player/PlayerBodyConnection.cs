namespace BIG.IncredibleCityCrisis
{

    public struct PlayerBodyConnection
    {

        public Player player;
        public Body body;

        public PlayerBodyConnection(Player player, Body body)
        {
            this.player = player;
            this.body = body;
        }

   }
}