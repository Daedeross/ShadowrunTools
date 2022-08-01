using ShadowrunTools.Characters.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShadowrunTools.Characters
{
    /// <summary>
    /// Interface for Traits which can be augmented (i.e. modified by <see cref="IAugment">Augments</see>).
    /// </summary>
    /// <remarks>
    /// Any trait that can be changed by an <see cref="IAugment">Augment</see>
    /// <em>must</em> implement this interface.
    /// See <see cref="LeveledTrait"/> for an abstract class that implements this.
    /// Most IAugmentable traits are descendants of <b>LeveledTrait</b>.
    /// </remarks>
    public interface IAugmentable : INotifyPropertyChanged,
        INamedItem /// Since Augments find their targets by name, every IAugmentable needs a name.
    {
        /// <summary>
        /// Collection of <see cref="IBonus"/>es that target this trait.
        /// </summary>
        /// <remarks>
        /// It is up to the implementing class to subscribe to this collection's
        /// CollectionChanged event.
        /// </remarks>
        IEnumerable<IBonus> Bonuses { get; }

        /// <summary>
        /// Add a bonus to the trait.
        /// </summary>
        /// <param name="bonus"></param>
        void AddBonus(IBonus bonus);

        /// <summary>
        /// Removes a <see cref="IBonus"/> from the trait.
        /// </summary>
        /// <param name="bonus"></param>
        void RemoveBonus(IBonus bonus);
    }
}

