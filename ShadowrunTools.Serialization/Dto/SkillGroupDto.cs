namespace ShadowrunTools.Serialization.Dto
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "SkillGroup", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SkillGroupDto : TraitDtoBase
    {
        [DataMember]
        public List<string> Skills { get; set; }
    }
}
