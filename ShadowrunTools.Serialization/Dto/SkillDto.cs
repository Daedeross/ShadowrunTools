using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Skill", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SkillDto : TraitDtoBase
    {
        [DataMember]
        public int BaseIncrease { get; set; }
        [DataMember]
        public int Improvement { get; set; }

        [DataMember]
        public List<string> Specializations { get; set; }
    }
}
