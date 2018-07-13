using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;

namespace ShadowrunTools.Characters
{
    public interface ICategorizedTraitContainer: IDictionary<string, ITraitContainer>
    {
        IEnumerable<ITrait> GetTraitsByName(string name);
    }
}
