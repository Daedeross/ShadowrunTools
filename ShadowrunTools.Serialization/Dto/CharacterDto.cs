namespace ShadowrunTools.Serialization
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Prototypes;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Character", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterDto// : ICharacterLoader
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        [DataMember]
        public GenerationMethod GenerationMethod { get; set; }

        //[DataMember]
        //public Dictionary<string, IPrototype> Prototypes { get; set; }

        [DataMember]
        public CharacterPrioritiesDto Priorities { get; set; }

        [DataMember]
        public CharacterMetatypeDto Metatype { get; set; }

        [DataMember]
        public SpecialChoiceDto SpecialChoice { get; set; }

        [DataMember]
        public Dictionary<string, AttributeDto> Attributes { get; set; }

        //ITraitContainer<ISkill> ActiveSkills { get; set; }
    }
}
