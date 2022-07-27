using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Trait", Namespace = "http://schemas.shadowruntools.com/loaders")]
    [KnownType(nameof(KnownTypes))]
    public abstract class TraitDtoBase
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Guid Id { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public TraitType TraitType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string UserNotes { get; set; }
        [DataMember]
        public string Book { get; set; }
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public int PrototypeHash { get; set; }

        private static Lazy<Type[]> _knownTypes = new Lazy<Type[]>(
            () =>
                Assembly.GetAssembly(typeof(TraitDtoBase))
                    .GetTypes()
                    .Where(t => typeof(TraitDtoBase).IsAssignableFrom(t))
                    .ToArray()
            );

        public static Type[] KnownTypes() => _knownTypes.Value;
    }
}
