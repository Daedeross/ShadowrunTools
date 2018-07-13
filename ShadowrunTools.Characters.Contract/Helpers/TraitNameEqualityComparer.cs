using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Helpers
{
    public class TraitNameEqualityComparer : IEqualityComparer<ITrait>
    {
        public static IEqualityComparer<ITrait> Default { get; } = new TraitNameEqualityComparer();

        public bool Equals(ITrait x, ITrait y)
        {
            return string.Equals(x.Name, y.Name);
        }

        public int GetHashCode(ITrait obj)
        {
            return obj.Name?.GetHashCode() ?? 0;
        }
    }
}
