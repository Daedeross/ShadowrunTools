using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Characters.Factories
{
    public class CharacterLoader : ICharacterLoader
    {
        private readonly ITraitLoader _loader;

        public CharacterLoader(ITraitLoader loader)
        {
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public ICharacter FromDto(CharacterDto dto)
        {
            var meta = _loader.FromDto(dto.Metatype);

            var characterPriorities = _loader.FromDto(dto.GenerationMethod, dto.Priorities);

            var character = new Character(meta, characterPriorities);
            character.Name = dto.Name;
            character.GenerationMethod = dto.GenerationMethod;

            foreach (var kvp in dto.Attributes)
            {
                character.Attributes[kvp.Key] = _loader.FromDto(character, kvp.Value);
            }

            // TODO: Special Choice, SpecialSkillChoice, Skills, Qualities, Gear, & Many More!

            return character;
        }

        public CharacterDto ToDto(ICharacter character)
        {
            var dto = new CharacterDto
            {
                GenerationMethod = character.GenerationMethod,
                Name = character.Name,
                Priorities = _loader.ToDto(character.Priorities),
                Metatype = _loader.ToDto(character.Metatype),
                Attributes = character.Attributes.ToDictionary(kvp => kvp.Key, kvp => _loader.ToDto(kvp.Value))

                // TODO: Special Choice, SpecialSkillChoice, Skills, Qualities, Gear, & Many More!
            };

            return dto;
        }
    }
}
