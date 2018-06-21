namespace ShadowrunTools.Characters.Model
{
    /// <summary>
    /// The possible types of character generation.
    /// </summary>
    public enum GenerationMethod
    {
        /// <summary>For GM use, no points, limitations, etc.</summary>
        NPC = 0,
        /// <summary>Default method from <em>SR5 - Core Rulebook</em> §3.</summary>
        Priority = 1,
        /// <summary>See <em>Run Faster</em> p.62.</summary>
        SumToTen = 2,
        /// <summary>See <em>Run Faster</em> p.64. Changed name from PointBuy to differentiate from <see cref="CharGenMethod.BuildPoints"/>.</summary>
        KarmaGen = 3,
        /// <summary>See <em>Run Faster</em> p.65</summary>
        LifeModules = 4,
        /// <summary>Added for possible SR4/house rules compatability.</summary>
        BuildPoints = 5,
    }
}
