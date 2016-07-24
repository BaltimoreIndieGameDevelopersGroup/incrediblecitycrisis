namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Every GameObject has a tag. This class gathers the string names of the
    /// tags used in the game.
    /// https://docs.unity3d.com/Manual/Tags.html
    /// </summary>
    public static class Tags
    {

        // The hero player position:
        public const string Hero = "Hero";

        // Survivors following the hero position:
        public const string Survivor = "Survivor";

        // Enemy players:
        public const string Enemy = "Enemy";

        // An item players can use:
        public const string Usable = "Usable";

        // Buildings and walls:
        public const string Environment = "Environment";

    }

}