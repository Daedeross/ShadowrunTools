namespace ShadowrunTools.Characters.Helpers
{
    using ShadowrunTools.Characters.Traits;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
