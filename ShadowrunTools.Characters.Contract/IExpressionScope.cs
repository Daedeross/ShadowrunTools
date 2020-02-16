using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IExpressionScope<out T>
    {
        public T Me { get; }
        public ITraitContainer<IAttribute> Attributes { get; }
    }
}
