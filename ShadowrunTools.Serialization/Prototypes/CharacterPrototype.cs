using System.Collections.Generic;
using System.Linq;
using ShadowrunTools.Characters.Prototypes;

namespace ShadowrunTools.Serialization.Prototypes
{
    public class CharacterPrototype : ICharacterPrototype
    {
        public List<IAttributePrototype> CoreAttributes { get; private set; }

        public List<ISkillPrototype> Skills { get; private set; } 

        public static ICharacterPrototype CreateFromRepository(IPrototypeRepository repository)
        {
            var attributes = repository.GetTraits<AttributePrototype>().Values;

            var coreAttributes = attributes.Where(attr => attr.SubCategory.Contains("Mental") || attr.SubCategory.Contains("Physical"));

            return new CharacterPrototype
            {
                CoreAttributes = coreAttributes.ToList<IAttributePrototype>(),
                Skills = repository.GetTraits<SkillPrototype>().Values.ToList<ISkillPrototype>(),
            };
        }
    }
}
