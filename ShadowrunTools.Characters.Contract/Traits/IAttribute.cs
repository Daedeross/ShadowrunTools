namespace ShadowrunTools.Characters.Traits
{
    /// <summary>
    /// Includes the core 8 Attributes (STR, CHA, etc.), Special Attributes (MAG or RES), and all the derrived ones.
    /// </summary>
    /// <remarks>
    /// Core Attributes: (BOD, AGI, REA, STR, WIL, LOG, INT, CHA)
    /// Special Attribute: (MAG or RES)
    /// Initiatives: (Meat, Matrix [Cold and Hot], Astral)
    /// Derrived Attributes: (Composure, etc.)
    /// Inherent Limits: (Physical, Mental, Social)
    /// Condition Monitors: (Physical and Stun, overflow)
    /// Living Persona: (implementations TBD)
    /// Reputation: (only in-play, TBD)
    /// </remarks>
    public interface IAttribute: ILeveledTrait
    {
        string ShortName { get; }
    }
}
