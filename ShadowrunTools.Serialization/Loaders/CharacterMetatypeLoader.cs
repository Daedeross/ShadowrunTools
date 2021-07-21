namespace ShadowrunTools.Serialization.Loaders
{
    using ShadowrunTools.Characters;
    using ShadowrunTools.Characters.Loaders;
    using System.Runtime.Serialization;

    [DataContract(Name = "CharacterMetatype", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterMetatypeLoader
    {
        [DataMember]
        public string Name { get; set; }

        public static CharacterMetatypeLoader Create(ICharacterMetatype metatype)
        {
            return new CharacterMetatypeLoader
            {
                Name = metatype.Name
            };
        }
    }
}