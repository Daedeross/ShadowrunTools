namespace ShadowrunTools.Characters.Traits
{
    using System.Collections.Generic;

    public interface ISkillGroup : ILeveledTrait
    {
        IEnumerable<string> Skills { get; set; }

        bool Broken { get; }
    }
}
