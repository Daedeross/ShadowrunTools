using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public interface ISpecialsPriority
    {
        IReadOnlyCollection<ISpecialOption> Options { get; }
    }
}
