using ShadowrunTools.Characters.Contract.Model;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Foundation;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "SkillPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class SkillPrototype : LeveledTraitPrototype, ISkillPrototype
    {
        private static int? _hash;

        [DataMember]
        public bool TrainedOnly { get; set; }

        [DataMember]
        public SkillType SkillType { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string LinkedAttribute { get; set; }

        [DataMember]
        public string UsualLimit { get; set; }

        [DataMember]
        public List<string> Specializations { get; set; }

        public SkillPrototype()
        {
            Category = Categories.Skills;
        }

        public override int GetHashCode()
        {
            if (!_hash.HasValue)
            {
                _hash = FNV1aHash.AppendHash32(base.GetHashCode(), TrainedOnly);
            }

            return _hash.Value;
        }
    }
}
