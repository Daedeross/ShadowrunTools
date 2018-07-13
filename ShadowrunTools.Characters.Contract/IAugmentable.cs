using ShadowrunTools.Characters.Model;
using System.Collections.ObjectModel;

namespace ShadowrunTools.Characters
{
    /// <summary>
    /// Intarface for Traits which can be augmented (i.e. modified by <see cref="IAugment">Augments</see>).
    /// </summary>
    /// <remarks>
    /// Any trait that can be changed by an <see cref="IAugment">Augment</see>
    /// <em>must</em> implement this interface.
    /// <see cref="LeveledTrait"/> for an abstract class that implements this.
    /// Most IAugmentable traits are descendants of <b>LeveledTrait</b>.
    /// </remarks>
    public interface IAugmentable : INotifyItemChanged,
        INamedItem /// Since Augments find their targets by name, every IAugmentable needs a name.
    {
        /// <summary>
        /// Collection of Augments that target this trait.
        /// </summary>
        /// <remarks>
        /// It is up to the implementing class to subscribe to this collection's
        /// CollectionChanged event.
        /// </remarks>
        ObservableCollection<IAugment> Augments { get; }

        /// <summary>
        /// Method to handle when a subscribed Augment's property changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// While it should actualy be up to the implementing class what to call
        /// this method, enforcing this name helps with descedant classes to override
        /// it. It is recomended to make this method virtual (or abstract).
        /// </remarks>
        void OnAugmentChanged(object sender, ItemChangedEventArgs e);

        /// <summary>
        /// To be used to handle Augments.CollectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAugmentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e);

        /// <summary>
        /// Callback called by IAugment when it is Disposed
        /// </summary>
        /// <param name="kind"></param>
        void OnAugmentRemoving(AugmentKind kind);
    }
}

