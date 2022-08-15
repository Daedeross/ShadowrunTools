using DynamicData.Binding;
using ShadowrunTools.Characters.Contract.Model;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Traits
{
    public interface ISkill: ILeveledTrait
    {
        /// <summary>
        /// The type of skill. <see cref="SkillType"/>
        /// </summary>
        SkillType SkillType { get; }

        /// <summary>
        /// The name of the skill group that contains the skill.
        /// <see cref="null"/> if skill is not in any group.
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// The attribute that is usually added to the pool when using the skill.
        /// </summary>
        IAttribute LinkedAttribute { get; }

        /// <summary>
        /// True if the skill can be used when rating is zero.
        /// </summary>
        bool AllowDefault { get; }
        
        /// <summary>
        /// Will probably be removed from ISkill, placed in the ViewModel
        /// </summary>
        int TotalPool { get; }

        /// <summary>
        /// Will probably be removed from ISkill, placed in the ViewModel
        /// </summary>
        int AugmentedPool { get; }

        /// <summary>
        /// The name of the limit the skill usually uses.
        /// </summary>
        string UsualLimit { get; }

        /// <summary>
        /// Specializations purchased.
        /// </summary>
        IObservableCollection<string> Specializations { get; }

        /// <summary>
        /// Suggested Specializations, loaded from prototype;
        /// </summary>
        IReadOnlyCollection<string> SuggestedSpecializations { get; }
    }
}
