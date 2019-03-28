using ShadowrunTools.Characters.Model;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    /// <summary>
    /// Represents a character's selected priorities.
    /// </summary>
    public interface ICharacterPriorities
    {
        /// <summary>
        /// The selected priority level for Metatypes.
        /// </summary>
        PriorityLevel MetatypePriority { get; set; }

        /// <summary>
        /// The selected priority level for Attributes.
        /// </summary>
        PriorityLevel AttributePriority { get; set; }

        /// <summary>
        /// The selected priority level for Specials.
        /// </summary>
        PriorityLevel SpecialPriority { get; set; }

        /// <summary>
        /// The selected priority level for Skills.
        /// </summary>
        PriorityLevel SkillPriority { get; set; }

        /// <summary>
        /// The selected priority level for Resources.
        /// </summary>
        PriorityLevel ResourcePriority { get; set; }

        /// <summary>
        /// The metatypes, and associated ancilaries) available to pick.
        /// </summary>
        IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions { get; }

        /// <summary>
        /// Update all Priority Levels at once.
        /// </summary>
        /// <param name="metatype"></param>
        /// <param name="attribute"></param>
        /// <param name="special"></param>
        /// <param name="skill"></param>
        /// <param name="resource"></param>
        void SetPriorities(PriorityLevel metatype, PriorityLevel attribute,
            PriorityLevel special, PriorityLevel skill, PriorityLevel resource);

        /// <summary>
        /// Points available to increase core attributes.
        /// </summary>
        int AttributePoints { get; }

        /// <summary>
        /// Max sum for all active skill ratings (non-grouped).
        /// </summary>
        int SkillPoints { get; }

        /// <summary>
        /// Max sum for all active skill groups.
        /// </summary>
        int SkillGroupPoints { get; }

        /// <summary>
        /// Amout of nuyen (¥) available to spend during creation.
        /// </summary>
        decimal Resources { get; }

        /// <summary>
        /// Used in SumToTen
        /// </summary>
        int TotalPriorityPoints { get; }
    }
}
