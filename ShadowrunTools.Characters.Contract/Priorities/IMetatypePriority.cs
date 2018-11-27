using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public interface IMetatypePriority
    {
        /// <summary>
        /// The metatypes, and associated ancilaries) available to pick.
        /// </summary>
        IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions { get; }
    }
}
