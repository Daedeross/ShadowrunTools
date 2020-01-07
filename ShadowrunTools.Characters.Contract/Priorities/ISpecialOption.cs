using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public interface ISpecialOption
    {
        string Quality { get; }

        string AttributeName { get; }

        int AttributeRating { get; }

        IReadOnlyCollection<ISpecialSkillChoice> SkillOptions { get; }

        IReadOnlyCollection<ISpecialSkillChoice> SkillGroupOptions { get; }

        int FreeSpells { get; }

        int FreeComplexForms { get; }
    }
}
