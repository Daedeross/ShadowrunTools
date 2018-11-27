using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public interface ISpecialOptions
    {
        string AttributeName { get; }

        int AttributeRating { get; }

        int SkillPoints { get; }

        IReadOnlyCollection<string> SkillOptions { get; }

        int SkillGroupPoints { get; }

        IReadOnlyCollection<string> SkillGroupOptions { get; }

        int FreeSpells { get; }

        int FreeComplexforms { get; }
    }
}
