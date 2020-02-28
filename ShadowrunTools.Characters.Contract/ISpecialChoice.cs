namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISpecialChoice
    {
        string Quality { get; }

        string AttributeName { get; }

        int AttributeRating { get; }

        ISpecialSkillChoice SkillOptions { get; }

        ISpecialSkillChoice SkillGroupOptions { get; }

        int FreeSpells { get; }

        int FreeComplexForms { get; }
    }
}
