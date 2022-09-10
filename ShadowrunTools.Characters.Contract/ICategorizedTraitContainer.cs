using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters
{
    public interface ICategorizedTraitContainer: IDictionary<string, ITraitContainer>, INotifyCollectionChanged
    {
        bool InPlay { get; }

        IEnumerable<ITrait> GetTraitsByName(string name);

        event EventHandler<TraitsChangedEventArgs> TraitsChanged;

        bool TryGetTrait(string category, string name, out ITrait trait);
    }
}
