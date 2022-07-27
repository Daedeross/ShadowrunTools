using ShadowrunTools.Characters.Model;
using ShadowrunTools.Foundation;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public class GamePriorities : IPriorities
    {
        private readonly Dictionary<PriorityLevel, IMetatypePriority> _metatype;
        public IReadOnlyDictionary<PriorityLevel, IMetatypePriority> Metatype => _metatype;

        private readonly Dictionary<PriorityLevel, IAttributesPriority> _attributes;
        public IReadOnlyDictionary<PriorityLevel, IAttributesPriority> Attributes => _attributes;

        private readonly Dictionary<PriorityLevel, ISpecialsPriority> _specials;
        public IReadOnlyDictionary<PriorityLevel, ISpecialsPriority> Specials => _specials;

        private readonly Dictionary<PriorityLevel, ISkillsPriority> _skills;
        public IReadOnlyDictionary<PriorityLevel, ISkillsPriority> Skills => _skills;

        private readonly Dictionary<PriorityLevel, IResourcesPriority> _resources;
        public IReadOnlyDictionary<PriorityLevel, IResourcesPriority> Resources => _resources;

        public GamePriorities(IPriorities source)
        {
            _metatype = source.Metatype.Copy();
            _attributes = source.Attributes.Copy();
            _specials = source.Specials.Copy();
            _skills = source.Skills.Copy();
            _resources = source.Resources.Copy();
        }
    }
}
