using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System.Diagnostics.CodeAnalysis;

namespace ShadowrunTools.Characters.Factories
{
    public interface ITraitFactory : IFactory
    {
        IAttribute CreateAttribute(ICharacter character, IAttributePrototype prototype);

        ISkill CreateSkill(ICharacter character, ISkillPrototype prototype);
    }
}
