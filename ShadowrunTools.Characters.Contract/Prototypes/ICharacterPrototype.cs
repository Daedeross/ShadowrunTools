using System.Collections.Generic;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface ICharacterPrototype
    {
        List<IAttributePrototype> CoreAttributes { get; }

        List<ISkillPrototype> Skills { get; }
    }
}
