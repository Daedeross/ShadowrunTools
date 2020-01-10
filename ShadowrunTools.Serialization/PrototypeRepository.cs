using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Serialization
{
    public class PrototypeRepository: IPrototypeRepository
    {
        private List<IMetavariantPrototype> _metavariants;
        public IReadOnlyCollection<IMetavariantPrototype> Metavariants => _metavariants;

        private IMetavariantPrototype _defaultMetavariant;
        public IMetavariantPrototype DefaultMetavariant
        {
            get
            {
                if (_defaultMetavariant is null)
                {
                    _defaultMetavariant = GetDefaultRace("Human");
                }
                return _defaultMetavariant;
            }
        }

        private PrioritiesPrototype _priorities = new PrioritiesPrototype();
        public IPriorities Priorities => _priorities;

        private readonly Dictionary<TraitType, Dictionary<string, ITraitPrototype>> _traitsMap1;
        private readonly Dictionary<Type, Dictionary<string, ITraitPrototype>> _traitsMap2;

        public PrototypeRepository()
        {
            _traitsMap1 = new Dictionary<TraitType, Dictionary<string, ITraitPrototype>>();
            _traitsMap2 = new Dictionary<Type, Dictionary<string, ITraitPrototype>>();

            _metavariants = new List<IMetavariantPrototype>();
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

        public IReadOnlyDictionary<string, TPrototype> GetTraits<TPrototype>()
            where TPrototype : class, ITraitPrototype
        {
            if (_traitsMap2.TryGetValue(typeof(TPrototype), out Dictionary<string, ITraitPrototype> inner))
            {
                return inner.ToDictionary(kvp => kvp.Key, kvp => kvp.Value as TPrototype);
            }
            return new Dictionary<string, TPrototype>();
        }

        public void MergeFile(PrototypeFile prototypeFile)
        {
            if (prototypeFile.Attributes != null)
            {
                MergeTraitCollection(prototypeFile.Attributes);
            }

            if (prototypeFile.Metavariants != null)
            {
                _metavariants.AddRange(prototypeFile.Metavariants); 
            }

            if (prototypeFile.Priorities != null)
            {
                _priorities.MergeWith(prototypeFile.Priorities);
            }
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

        public IMetavariantPrototype GetDefaultRace(string name)
        {
            return Metavariants.First(mv => mv.Name == name);
        }
    }
}
