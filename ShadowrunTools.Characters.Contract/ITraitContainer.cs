namespace ShadowrunTools.Characters
{
    using System.Collections.Generic;

    public interface ITraitContainer : IDictionary<string, ITrait>
    {
        bool OwnsObjects { get; set; }
    }
}
