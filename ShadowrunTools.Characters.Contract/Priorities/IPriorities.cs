using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Priorities
{
    public interface IPriorities
    {
        IReadOnlyDictionary<PriorityLevel, IMetatypePriority> Metatype { get; }

        IReadOnlyDictionary<PriorityLevel, IAttributesPriority> Attributes { get; }

        IReadOnlyDictionary<PriorityLevel, ISpecialsPriority> Specials { get; }

        IReadOnlyDictionary<PriorityLevel, ISkillsPriority> Skills { get; }

        IReadOnlyDictionary<PriorityLevel, IResourcesPriority> Resources { get; }
    }
}
