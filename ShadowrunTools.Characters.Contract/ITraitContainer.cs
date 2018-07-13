using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters
{
    public interface ITraitContainer : IDictionary<string, ITrait>, INotifyCollectionChanged
    {
        bool OwnsObjects { get; set; }
    }
}
