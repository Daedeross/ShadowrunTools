namespace ShadowrunTools.Serialization
{
    using System.Runtime.Serialization;

    [DataContract(Name = "CharacterMetatype", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterMetatypeDto
    {
        [DataMember]
        public string Name { get; set; }
    }
}