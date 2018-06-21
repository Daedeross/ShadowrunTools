namespace ShadowrunTools.Characters.Traits
{
    using ShadowrunTools.Characters.Contract.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
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
