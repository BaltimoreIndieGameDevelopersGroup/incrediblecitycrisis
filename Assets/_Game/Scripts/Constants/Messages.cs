namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Unity's SendMessage and BroadcastMessage methods use invoke methods on 
    /// target GameObjects by string-based method name. This class gathers
    /// those strings so we don't use string literals everywhere.
    /// https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html
    /// https://docs.unity3d.com/ScriptReference/GameObject.BroadcastMessage.html
    /// </summary>
    public static class Messages
    {

        // OnAttachPlayer(PlayerBodyConnection): Called on Player & Body when a Player attaches to a Body.
        public const string OnAttachPlayer = "OnAttachPlayer";

        // OnDetachPlayer(PlayerBodyConnection): Called when a Player detaches from a Body.
        public const string OnDetachPlayer = "OnDetachPlayer";

        // OnTakeDamage(DamageInfo): Called when a GameObject takes damage.
        public const string OnTakeDamage = "OnTakeDamage";

        // OnHealthChange(float): Called when something gains or loses health.
        public const string OnHealthChange = "OnHealthChange";

        // OnDie(int killedByPlayerNumber): Called when a body is killed.
        public const string OnDie = "OnDie";

    }
}