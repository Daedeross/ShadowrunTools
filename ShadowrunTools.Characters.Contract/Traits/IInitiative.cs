using System.Collections.Generic;

namespace ShadowrunTools.Characters.Traits
{
    public interface IInitiative: IAttribute
    {
        IEnumerable<IAttribute> Attributes { get; }

        int BaseDice { get; }
        int BonusDice { get; }
        int AugmentedDice { get; }
    }
}
