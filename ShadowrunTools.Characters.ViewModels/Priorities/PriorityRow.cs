using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PriorityRow : ViewModelBase
    {
        public PriorityLevel Level { get; private set; }

        public PriorityCell Metatype { get; set; }

        public PriorityCell Attributes { get; set; }

        public PriorityCell Specials { get; set; }

        public PriorityCell Skills { get; set; }

        public PriorityCell Resources { get; set; }

        public PriorityRow(DisplaySettings displaySettings,
            PriorityLevel level,
            IPriorities priorities)
            : base(displaySettings)
        {
            Level = level;

            Metatype = new PriorityCell(displaySettings, GetMetatypeItems(level, priorities));
            Attributes = new PriorityCell(displaySettings, GetAttributesItems(level, priorities));
            Specials = new PriorityCell(displaySettings, GetSpecialsItems(level, priorities));
            Skills = new PriorityCell(displaySettings, GetSkillsItems(level, priorities));
            Resources = new PriorityCell(displaySettings, GetResourcesItems(level, priorities));
        }

        private static List<string> GetMetatypeItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Metatype.TryGetValue(level, out var metas))
            {
                return new List<string>();
            }

            return metas.MetavariantOptions.Select(opt => opt.AdditionalKarmaCost == 0
                ? $"{opt.Metavariant}-{opt.SpecialAttributePoints}"
                : $"{opt.Metavariant}-{opt.SpecialAttributePoints} (-{opt.AdditionalKarmaCost})")
                .ToList();
        }

        private static List<string> GetAttributesItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Attributes.TryGetValue(level, out var attrs))
            {
                return new List<string>();
            }

            return new List<string> { attrs.AttibutePoints.ToString() };
        }

        private static List<string> GetSpecialsItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Specials.TryGetValue(level, out var specials))
            {
                return new List<string>();
            }

            return new List<string> { };
        }

        private static List<string> GetSkillsItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Skills.TryGetValue(level, out var skills))
            {
                return new List<string>();
            }

            return new List<string> { $"{skills.SkillPoints}/{skills.SkillGroupPoints}" };
        }

        private static List<string> GetResourcesItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Resources.TryGetValue(level, out var resources))
            {
                return new List<string>();
            }

            return new List<string> { resources.Resources.ToString() };
        }
    }
}
