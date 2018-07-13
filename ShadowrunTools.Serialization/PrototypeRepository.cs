using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Serialization
{
    public class PrototypeRepository: IPrototypeRepository
    {
        public List<IMetavariantPrototype> Metavariants { get; set; }

        private readonly Dictionary<TraitType, Dictionary<string, ITraitPrototype>> _traitsMap1;
        private readonly Dictionary<Type, Dictionary<string, ITraitPrototype>> _traitsMap2;

        public PrototypeRepository()
        {
            _traitsMap1 = new Dictionary<TraitType, Dictionary<string, ITraitPrototype>>();
            _traitsMap2 = new Dictionary<Type, Dictionary<string, ITraitPrototype>>();
        }

        public ITraitPrototype GetTraitPrototype(TraitType traitType, string name)
        {
            if (_traitsMap1.TryGetValue(traitType, out Dictionary<string, ITraitPrototype> inner))
            {
                inner.TryGetValue(name, out ITraitPrototype prototype);
                return prototype;
            }
            return default;
        }

        TPrototype IPrototypeRepository.GetTraitPrototype<TPrototype>(string name)
        {
            if (_traitsMap2.TryGetValue(typeof(TPrototype), out Dictionary<string, ITraitPrototype> inner))
            {
                inner.TryGetValue(name, out ITraitPrototype prototype);
                return prototype as TPrototype;
            }
            return default;
        }

        public void MergeTraitCollection(IEnumerable<ITraitPrototype> collection)
        {
            var groups1 = collection.GroupBy(proto => proto.TraitType);
            foreach (var group in groups1)
            {
                if (!_traitsMap1.TryGetValue(group.Key, out Dictionary<string, ITraitPrototype> inner))
                {
                    inner = new Dictionary<string, ITraitPrototype>();
                    _traitsMap1[group.Key] = inner;
                }
                foreach (var proto in group)
                {
                    inner[proto.Name] = proto;
                }
            }

            var groups2 = collection.GroupBy(proto => proto.GetType());
            foreach (var group in groups2)
            {
                if (!_traitsMap2.TryGetValue(group.Key, out Dictionary<string, ITraitPrototype> inner))
                {
                    inner = new Dictionary<string, ITraitPrototype>();
                    _traitsMap2[group.Key] = inner;
                }
                foreach (var proto in group)
                {
                    inner[proto.Name] = proto;
                }
            }
        }
    }
}
