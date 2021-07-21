namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Prototypes;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public interface ICharacterPersistence
    {
        // TODO: Commented out methods are still being waffled
        void SaveCharacter(string filename, ICharacter character);

        void SaveCharacter(Stream stream, ICharacter character);

        //string SerializeCharacterCharacter(ICharacter character);

        //byte[] SerializeCharacterCharacter(ICharacter character, System.Text.Encoding encoding);

        ICharacter LoadCharacter(string filename, IPrototypeRepository prototypeRepository);

        //ICharacter LoadCharacter(Stream stream, IPrototypeRepository prototypeRepository);

        //ICharacter DserializeCharacterCharacter(string text, IPrototypeRepository prototypeRepository);

        //ICharacter DeserializeCharacterCharacter(byte[] data, System.Text.Encoding encoding, IPrototypeRepository prototypeRepository);
    }
}
