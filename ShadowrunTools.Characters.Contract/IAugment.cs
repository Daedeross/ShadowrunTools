using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
    public interface IAugment : IDisposable, INotifyPropertyChanged
    {
        IEnumerable<IAugmentable> Targets { get; }

        double Amount { get; }
    }
}
