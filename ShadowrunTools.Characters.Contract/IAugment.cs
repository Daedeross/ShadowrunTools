namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;
    using System;

    /// <summary>
    /// Interface that represents something that can modify another trait
    /// </summary>
    /// <remarks>
    /// Each target trait and property must be a sepparate IAugment. Example:
    /// The <b>Improved Reflexes</b> adept power gives +1 to Reaction and +1 to
    /// Physical Initiative Dice per level. That means that <b>Improved Reflexes</b>
    /// needs two augments, one for the reaction bonus, and one for the extra dice.
    /// 
    /// It is up to the targeted trait (which must implement <see cref="IAugmentable"/>)
    /// how to handle the Augment.
    /// </remarks>
    public interface IAugment: IDisposable
    {
        IAugmentable Target { get; set; }

        /// <summary>
        /// See <see cref="AugmentKind"/>. Essentialy determines what property is modified.
        /// </summary>
        AugmentKind Kind { get; }

        /// <summary>
        /// Array indexed by the owner's Rating to determin the  bonus to give.
        /// If owner does not have a rating or it is out of range for the array,
        /// Index zero [0] is used.
        /// </summary>
        decimal[] BonusArray { get; set; }

        decimal Bonus { get;}

        /// <summary>
        /// The name of the Trait that this Augment modifies.
        /// </summary>
        string TargetName { get; }
    }
}
