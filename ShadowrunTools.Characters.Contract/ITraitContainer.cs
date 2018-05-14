namespace ShadowrunTools.Characters
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface ITraitContainer : IDictionary<string, ITrait>, INotifyCollectionChanged
    {
        bool OwnsObjects { get; set; }
    }
}
