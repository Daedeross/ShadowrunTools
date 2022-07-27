using System.Collections.Generic;

namespace ShadowrunTools.Characters.Traits
{
    public interface IInitiative: ICompositeAttribute
    {
        int BaseDice { get; }
        int BonusDice { get; }
        int AugmentedDice { get; }
    }
}
