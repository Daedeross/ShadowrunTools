namespace ShadowrunTools.Characters.Helpers
{
    using ShadowrunTools.Characters.Traits;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TraitNameComparer : IComparer<ITrait>
    {
        public static IComparer<ITrait> Default { get; } = new TraitNameComparer();

        public int Compare(ITrait x, ITrait y)
        {
            return string.Compare(x?.Name, y?.Name);
        }
    }
}
