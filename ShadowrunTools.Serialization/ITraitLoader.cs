using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Serialization
{
    public interface ITraitLoader
    {
        #region In

        /// <summary>
        /// Loads a Attribute onto a character from a seralizable dto.
        /// </summary>
        /// <param name="dto">The deserialized dto to load from.</param>
        /// <returns>The new <see cref="IAttribute"/></returns>
        IAttribute FromDto(ICharacter owner, AttributeDto dto);

        ICharacterMetatype FromDto(CharacterMetatypeDto dto);

        ICharacterPriorities FromDto(GenerationMethod method, CharacterPrioritiesDto dto);

        ISpecialChoice FromDto(SpecialChoiceDto dto);

        ISpecialSkillChoice FromDto(SpecialSkillChoiceDto dto);

        IQuality FromDto(QualityDto dto);

        #endregion

        #region Out

        /// <summary>
        /// Saves an <see cref="IAttribute"/> to a seralizable Dto.
        /// </summary>
        /// <param name="attribute">The attribute to save.</param>
        /// <returns>The Dto</returns>
        AttributeDto ToDto(IAttribute attribute);

        CharacterMetatypeDto ToDto(ICharacterMetatype metatype);

        CharacterPrioritiesDto ToDto(ICharacterPriorities priorities);

        SpecialChoiceDto ToDto(ISpecialChoice specialChoice);

        SpecialSkillChoiceDto ToDto(ISpecialSkillChoice specialSkillChoice);

        QualityDto ToDto(IQuality quality);

        #endregion
    }
}
