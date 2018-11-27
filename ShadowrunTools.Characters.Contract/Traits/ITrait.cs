using ShadowrunTools.Characters.Model;
using System;

namespace ShadowrunTools.Characters.Traits
{
    public interface ITrait: INamedItem, IEditable, IDisposable, IEquatable<ITrait> 
    {
        /// <summary>
        /// A unique Id of the trait,
        /// used for indirect referencing and serialziation
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// If true, the trait can be add or removed by the user.
        /// If false, the trait was added by another entity and is dependant on that entity.
        /// (eg. Qualities, Skill, and Spells granted in the 'Special' column in the Priority Table)
        /// </summary>
        bool Independant { get; }

        /// <summary>
        /// The Category the trait fits in.
        /// Also equivalent to the name of the enclosing ITraitConatiner.
        /// (eg: Active Skill, Attribute, Adept Power, Gear, etc)
        /// </summary>
        string Category { get; }

        /// <summary>
        /// The Sub-Category of the trait.
        /// (eg: Social Active Skill, Healing Spell, Ranged Weapon, etc.)
        /// </summary>
        string SubCategory { get; set; }

        /// <summary>
        /// Free-form text the user may enter.
        /// </summary>
        string UserNotes { get; set; }

        TraitType TraitType { get; }

        #region Reference Info
        string Book { get; set; }
        int Page { get; set; }
        #endregion // Reference Info
    }
}
