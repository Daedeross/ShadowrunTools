using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Attribute", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class AttributeDto: TraitDtoBase
    {
        [DataMember]
        public int BaseIncrease { get; set; }
        [DataMember]
        public int Improvement { get; set; }

        [DataMember]
        public string CustomOrder { get; set; }
    }
}
