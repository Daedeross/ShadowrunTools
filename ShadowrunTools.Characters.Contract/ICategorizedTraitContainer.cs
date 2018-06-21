namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Traits;
    using System.Collections.Generic;

    public interface ICategorizedTraitContainer: IDictionary<string, ITraitContainer>
    {
        IEnumerable<ITrait> GetTraitsByName(string name);
    }
}
