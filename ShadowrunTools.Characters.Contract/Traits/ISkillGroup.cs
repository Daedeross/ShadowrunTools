namespace ShadowrunTools.Characters.Traits
{
    using DynamicData.Binding;
    using System.Collections.Generic;

    public interface ISkillGroup : ILeveledTrait
    {
        IReadOnlyList<string> SkillNames { get; }

        bool Broken { get; }
    }
}
