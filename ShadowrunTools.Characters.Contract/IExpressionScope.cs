using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IExpressionScope<out T>
    {
        T Me { get; }
        ITraitContainer<IAttribute> Attributes { get; }
        ITraitContainer<ISkill> Skills { get; }
        ITraitContainer<IQuality> Qualities { get; }
    }
}
