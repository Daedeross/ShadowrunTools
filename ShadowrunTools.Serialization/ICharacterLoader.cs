using ShadowrunTools.Characters;
using ShadowrunTools.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Serialization
{
    public interface ICharacterLoader
    {
        ICharacter FromDto(CharacterDto loader);

        CharacterDto ToDto(ICharacter character);
    }
}
