namespace ShadowrunTools.Characters.Priorities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MetatypePriority : IMetatypePriority
    {
        public IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions => throw new NotImplementedException();
    }
}
