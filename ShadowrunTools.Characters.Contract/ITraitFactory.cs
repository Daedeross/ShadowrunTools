using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;

namespace ShadowrunTools.Characters
{
    public interface ITraitFactory
    {
        IAttribute CreateAttribute(ICategorizedTraitContainer container, IAttributePrototype prototype);
    }
}
