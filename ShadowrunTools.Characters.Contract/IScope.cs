using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IScope<out T>
        where T : class, INamedItem
    {
        T Owner { get; }

        T Me { get; }

        ICategorizedTraitContainer Traits { get; }
    }
}
