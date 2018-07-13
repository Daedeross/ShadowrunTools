using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Helpers
{
    public class TraitNameComparer : IComparer<ITrait>
    {
        public static IComparer<ITrait> Default { get; } = new TraitNameComparer();

        public int Compare(ITrait x, ITrait y)
        {
            return string.Compare(x?.Name, y?.Name);
        }
    }
}
