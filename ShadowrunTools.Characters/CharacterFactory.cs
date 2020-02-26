using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public static class CharacterFactory
    {
        public static Character Create(IRules rules, IPrototypeRepository prototypes, ITraitFactory traitFactory)
        {
            var characterPrototype = CharacterPrototype.CreateFromRepository(prototypes);
            var meta = prototypes.Metavariants.First(m => m.Name == "Elf");
            var metavariant = new CharacterMetatype(meta);
            ICharacterPriorities characterPriorities = null;
            switch (rules.GenerationMethod)
            {
                case Model.GenerationMethod.NPC:
                    throw new NotImplementedException();
                case Model.GenerationMethod.Priority:
                    characterPriorities = new CharacterPriorities(prototypes.Priorities);
                    break;
                case Model.GenerationMethod.SumToTen:
                    characterPriorities = new CharacterPointPriorities(prototypes.Priorities);
                    break;
                case Model.GenerationMethod.KarmaGen:
                    throw new NotImplementedException();
                case Model.GenerationMethod.LifeModules:
                    throw new NotImplementedException();
                case Model.GenerationMethod.BuildPoints:
                    throw new NotImplementedException();
                default:
                    break;
            }

            var character = new Character(traitFactory, metavariant, characterPriorities);
            foreach (var attributePrototype in characterPrototype.CoreAttributes)
            {
                character.Attributes[attributePrototype.Name] = character.CreateAttribute(attributePrototype);
            }

            return character;
        }
    }
}
