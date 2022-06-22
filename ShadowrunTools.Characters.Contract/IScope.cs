using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IScope<out T>
    {
        T Me { get; }

        ICategorizedTraitContainer Traits { get; }
    }
}
