using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using System;
using System.Collections.Generic;

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
    }
}
