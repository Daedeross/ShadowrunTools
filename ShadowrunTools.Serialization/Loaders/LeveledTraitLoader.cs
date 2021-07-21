using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "LeveledTrait", Namespace = "http://schemas.shadowruntools.com/loaders")]
    [KnownType(typeof(AttributeLoader))]
    public abstract class LeveledTraitLoader: TraitLoaderBase
    {
        [DataMember]
        public int ExtraMin { get; set; }
        [DataMember]
        public int ExtraMax { get; set; }
        [DataMember]
        public int BaseRating { get; set; }
        [DataMember]
        public int Improvement { get; set; }
    }
}
