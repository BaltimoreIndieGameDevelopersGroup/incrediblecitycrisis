namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Every GameObject has a tag. This class gathers the string names of the
    /// tags used in the game.
    /// https://docs.unity3d.com/Manual/Tags.html
    /// </summary>
    public static class Tags
    {

        // Hero body:
        public const string Hero = "Hero";

        // Normal enemy body: (not a possessed body such as a mech)
        public const string Enemy = "Enemy";

        // An item players can use:
        public const string Usable = "Usable";

    }

}