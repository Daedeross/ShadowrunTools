using System.Collections.Generic;

namespace ShadowrunTools.Characters.Traits
{
    public interface IInherentLimit : IAttribute
    {
        IEnumerable<IAttribute> PrimaryAttributes { get; }
        IEnumerable<IAttribute> SecondaryAttributes { get; }
    }
}
