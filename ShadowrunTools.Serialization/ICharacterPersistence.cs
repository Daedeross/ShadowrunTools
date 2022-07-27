namespace ShadowrunTools.Serialization
{
    using ShadowrunTools.Characters;
    using System.IO;

    public interface ICharacterPersistence
    {
        // TODO: Commented out methods are still being waffled
        void SaveCharacter(string filename, ICharacterLoader loader, ICharacter character);

        void SaveCharacter(Stream stream, ICharacterLoader loader, ICharacter character);

        //string SerializeCharacter(ICharacter character, ICharacterLoader loader);

        //byte[] SerializeCharacter(ICharacter character, ICharacterLoader loader, System.Text.Encoding encoding);

        ICharacter LoadCharacter(string filename, ICharacterLoader loader);

        //ICharacter LoadCharacter(Stream stream, ICharacterLoader loader);

        //ICharacter DserializeCharacterCharacter(string text, ICharacterLoader loader);

        //ICharacter DeserializeCharacterCharacter(byte[] data, System.Text.Encoding encoding, ICharacterLoader loader);
    }
}
