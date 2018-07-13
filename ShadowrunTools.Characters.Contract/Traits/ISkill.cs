using ShadowrunTools.Characters.Contract.Model;

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
    }
}
