using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Linq;

namespace ShadowrunTools.Characters.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly ITraitFactory _traitFactory;
        private readonly IRules _rules;

        public CharacterFactory(IRules rules, ITraitFactory traitFactory)
        {
            _rules = rules;
            _traitFactory = traitFactory;
        }

        public ICharacter Create(IPrototypeRepository prototypes, ICharacterMetatype metavariant)
        {
            var characterPrototype = CharacterPrototype.CreateFromRepository(prototypes);


            ICharacterPriorities characterPriorities = null;
            switch (_rules.GenerationMethod)
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

            var character = new Character(metavariant, characterPriorities);
            character.GenerationMethod = _rules.GenerationMethod;
            foreach (var attributePrototype in characterPrototype.CoreAttributes)
            {
                character.Attributes[attributePrototype.Name] = _traitFactory.CreateAttribute(character, attributePrototype);
            }

            character.Statuses.Add(Validators.ValidatorFactory.AttributePointsValidator(character));

            return character;
        }

        public ICharacter Create(IPrototypeRepository prototypes, string metavariantName)
        {
            var meta = prototypes.Metavariants.First(m => m.Name == metavariantName);
            var metavariant = new CharacterMetatype(meta);

            return Create(prototypes, metavariant);
        }

        public ICharacter Create(IPrototypeRepository prototypes)
        {
            return Create(prototypes, "Elf");// TODO: This is test code.
        }
    }
}
