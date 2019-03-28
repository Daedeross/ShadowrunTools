using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Serialization.Prototypes
{
    public class PrioritiesPrototype
    {
        public Dictionary<PriorityLevel, IMetatypePriority> Metatype { get; set; }

        public Dictionary<PriorityLevel, IAttributesPriority> Attributes { get; set; }

        public Dictionary<PriorityLevel, ISpecialsPriority> Specials { get; set; }

        public Dictionary<PriorityLevel, ISkillsPriority> Skills { get; set; }

        public Dictionary<PriorityLevel, IResourcesPriority> Resources { get; set; }
    }
}
