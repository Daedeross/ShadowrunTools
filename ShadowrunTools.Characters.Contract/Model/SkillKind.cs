using System;

namespace ShadowrunTools.Characters.Contract.Model
{
    /// <summary>
    /// The general types of skills.
    /// </summary>
    [Flags]
    public enum SkillType
    {
        NA        = 0,
        Active    = 1,
        Magical   = 1 << 1,
        Resonance = 1 << 2,
        Knowledge = 1 << 3,
        Language  = 1 << 4,
        MagicalActive = Active | Magical,
        ResonanceActive = Active | Resonance,
    }
}