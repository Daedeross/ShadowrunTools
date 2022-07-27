﻿using ShadowrunTools.Characters.Model;
using System;

namespace ShadowrunTools.Characters
{
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
    public interface IAugment : IDisposable, INotifyItemChanged
    {
        IAugmentable Target { get; set; }

        /// <summary>
        /// See <see cref="AugmentKind"/>. Essentialy determines what property on the target <see cref="IAugmentable"/> should be modified.
        /// </summary>
        AugmentKind Kind { get; }

        /// <summary>
        /// Array indexed by the owner's Rating to determine the bonus to give.
        /// If owner does not have a rating or it is out of range for the array,
        /// Index zero [0] is used.
        /// </summary>
        double[] BonusArray { get; set; }

        double Bonus { get; }

        /// <summary>
        /// The name of the Trait that this Augment modifies.
        /// </summary>
        string TargetName { get; }
    }
}
