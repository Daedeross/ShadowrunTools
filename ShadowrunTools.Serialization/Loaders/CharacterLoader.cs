namespace ShadowrunTools.Serialization.Loaders
{
    using ShadowrunTools.Characters.Loaders;
    using ShadowrunTools.Characters.Prototypes;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Character", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterLoader// : ICharacterLoader
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        //[DataMember]
        //public Dictionary<string, IPrototype> Prototypes { get; set; }

        [DataMember]
        public CharacterPrioritiesLoader Priorities { get; set; }

        [DataMember]
        public CharacterMetatypeLoader Metatype { get; set; }

        [DataMember]
        public SpecialChoiceLoader SpecialChoice { get; set; }

        [DataMember]
        public Dictionary<string, AttributeLoader> Attributes { get; set; }

        //ITraitContainer<ISkill> ActiveSkills { get; set; }
    }
}
