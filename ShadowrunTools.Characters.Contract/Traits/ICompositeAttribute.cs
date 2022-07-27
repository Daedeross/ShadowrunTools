using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Traits
{
    public interface ICompositeAttribute : IAttribute
    {
        IEnumerable<IAttribute> Attributes { get; }
    }
}
