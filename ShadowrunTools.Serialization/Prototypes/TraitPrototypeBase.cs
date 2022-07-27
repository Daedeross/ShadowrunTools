using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "TraitPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    [KnownType(nameof(KnownTypes))]
    public abstract class TraitPrototypeBase: ITraitPrototype
    {
        private static int? _hash;

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public Guid Id { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public TraitType TraitType { get; set; }
        [DataMember]
        public string Name { get; set; }
        // Set by implementing class
        public string Category { get; protected set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string Book { get; set; }
        [DataMember]
        public int Page { get; set; }

        public override int GetHashCode()
        {
            if (!_hash.HasValue)
            {
                _hash = FNV1aHash.CalculateHash32(TraitType, Name, Category, SubCategory, Book, Page);
            }

            return _hash.Value;
        }

        private static Lazy<Type[]> _knownTypes = new Lazy<Type[]>(
            () =>
                Assembly.GetAssembly(typeof(TraitPrototypeBase))
                    .GetTypes()
                    .Where(t => typeof(TraitPrototypeBase).IsAssignableFrom(t))
                    .ToArray()
            );
        public static Type[] KnownTypes() => _knownTypes.Value;
    }
}
