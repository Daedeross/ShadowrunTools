using ShadowrunTools.Characters.Contract.Model;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Traits
{
    public interface ISkill: ILeveledTrait
    {
        SkillType SkillType { get; }
        string GroupName { get; }
        string LinkedAttributeName { get; }
        IAttribute LinkedAttribute { get; }
        int TotalPool { get; }
        int AugmentedPool { get; }
        string UsualLimit { get; }
        IList<string> Specializations { get; }
    }
}
