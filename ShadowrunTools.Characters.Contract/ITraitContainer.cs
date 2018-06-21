namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Traits;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface ITraitContainer : IDictionary<string, ITrait>, INotifyCollectionChanged
    {
        bool OwnsObjects { get; set; }
    }
}
