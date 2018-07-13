using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "LeveledTrait", Namespace = "http://schemas.shadowruntools.com/loaders")]
    [KnownType(typeof(AttributeLoader))]
    public abstract class LeveledTraitLoader: TraitLoaderBase
    {
        [DataMember]
        int ExtraMin { get; set; }
        [DataMember]
        int ExtraMax { get; set; }
        [DataMember]
        int BaseRating { get; set; }
        [DataMember]
        int Improvement { get; set; }
    }
}
