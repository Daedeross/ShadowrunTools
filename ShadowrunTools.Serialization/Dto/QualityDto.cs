using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Quality", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class QualityDto : TraitDtoBase
    {
        public int Rating { get; set; }
    }
}
