using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System.Diagnostics.CodeAnalysis;

namespace ShadowrunTools.Characters
{
    public interface ITraitFactory
    {
        IAttribute CreateAttribute(ICharacter character, IAttributePrototype prototype);

        ISkill CreateSkill(ICharacter character, ISkillPrototype prototype);
    }
}
