using ShadowrunTools.Characters.Model;
using System;
using System.ComponentModel;

namespace ShadowrunTools.Characters.Traits
{
    /// <summary>
    /// Represents any trait on a character. A trait is almost anything numerical or textual on a character except its name, priorities, and metatype.
    /// </summary>
    /// <remarks>
    /// <b>Types of traits</b>
    /// Attribute: Things that are Core or derived from other attributes and Metatype. <seealso cref="IAttribute"/>
    /// Limit: Inherent limits (SR5 pp 46-47).
    /// Skill: What it says on the tin (SR5 p128). <seealso cref="ISkill"/>
    /// SkillGroup: What it says on the tin (SR5 p129).
    /// Quality: Qualities help round out your character’s personality while also providing a range of benefits or penalties.(SR5 p71)
    /// Spell: Known spells (Ritual and Spellcasting) for awakend characters.
    /// Power: Adept Powers.
    /// ComplexForm: For technomancers.
    /// Gear: Stuff that costs Items, weapons, foci, implants, lifestyle, etc
    /// GearMod: A modification to Gear, does not exist by itself and is "part of" a gear.
    /// </remarks>
    public interface ITrait: INamedItem, IEditable, IDisposable, IEquatable<ITrait> , INotifyPropertyChanged
    {
        /// <summary>
        /// A unique Id of the trait,
        /// used for indirect referencing and serialziation
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// If true, Trait should not be show in UI.
        /// </summary>
        bool Hidden { get; }

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
