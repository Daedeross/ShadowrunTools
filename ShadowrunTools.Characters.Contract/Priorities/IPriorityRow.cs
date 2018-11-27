using ShadowrunTools.Characters.Model;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    /// <summary>
    /// Represents a row on the Priority Table
    /// </summary>
    public interface IPriorityRow
    {
        /// <summary>
        /// The priority level this row represents (A-E)
        /// </summary>
        PriorityLevel Level { get; }

        /// <summary>
        /// The metatypes, and associated ancilaries) available to pick at for this level.
        /// </summary>
        IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions { get; }

        /// <summary>
        /// Points available to increase core attributes.
        /// </summary>
        int AttibutePoints { get; }

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
    }
}
