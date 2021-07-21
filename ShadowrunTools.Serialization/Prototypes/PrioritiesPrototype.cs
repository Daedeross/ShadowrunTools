using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Serialization.Prototypes.Priorities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "PrioritiesPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class PrioritiesPrototype : IPriorities
    {
        public PrioritiesPrototype()
        {
            MetatypePrototypes = new Dictionary<PriorityLevel, MetatypePriorityPrototype>();
            AttributesPrototypes = new Dictionary<PriorityLevel, AttributesPriorityPrototype>();
            SpecialsPrototyes = new Dictionary<PriorityLevel, SpecialsPriorityPrototype>();
            SkillsPrototypes = new Dictionary<PriorityLevel, SkillsPriorityPrototype>();
            ResourcesPrototypes = new Dictionary<PriorityLevel, ResourcesPriorityPrototype>();

            Initialize();
        }

        private void Initialize()
        {
            MetatypePrototypes[PriorityLevel.A] = new MetatypePriorityPrototype { };
            MetatypePrototypes[PriorityLevel.B] = new MetatypePriorityPrototype { };
            MetatypePrototypes[PriorityLevel.C] = new MetatypePriorityPrototype { };
            MetatypePrototypes[PriorityLevel.D] = new MetatypePriorityPrototype { };
            MetatypePrototypes[PriorityLevel.E] = new MetatypePriorityPrototype { };
            SpecialsPrototyes[PriorityLevel.A] = new SpecialsPriorityPrototype { };
            SpecialsPrototyes[PriorityLevel.B] = new SpecialsPriorityPrototype { };
            SpecialsPrototyes[PriorityLevel.C] = new SpecialsPriorityPrototype { };
            SpecialsPrototyes[PriorityLevel.D] = new SpecialsPriorityPrototype { };
            SpecialsPrototyes[PriorityLevel.E] = new SpecialsPriorityPrototype { };
        }

        #region Serialized
        #pragma warning disable IDE1006 // Naming Styles
        [DataMember(Name = "Metatype", EmitDefaultValue = false)]
        public Dictionary<PriorityLevel, MetatypePriorityPrototype> MetatypePrototypes { get; set; }

        [DataMember(Name = "Attributes", EmitDefaultValue = false)]
        public Dictionary<PriorityLevel, AttributesPriorityPrototype> AttributesPrototypes { get; set; }

        [DataMember(Name = "Specials", EmitDefaultValue = false)]
        public Dictionary<PriorityLevel, SpecialsPriorityPrototype> SpecialsPrototyes { get; set; }

        [DataMember(Name = "Skills", EmitDefaultValue = false)]
        public Dictionary<PriorityLevel, SkillsPriorityPrototype> SkillsPrototypes { get; set; }

        [DataMember(Name = "Resources", EmitDefaultValue = false)]
        public Dictionary<PriorityLevel, ResourcesPriorityPrototype> ResourcesPrototypes { get; set; }
        #pragma warning restore IDE1006 // Naming Styles
        #endregion

        #region IPriorities Implementation
        private IReadOnlyDictionary<PriorityLevel, IMetatypePriority> _metatype;
        private IReadOnlyDictionary<PriorityLevel, IAttributesPriority> _attributes;
        private IReadOnlyDictionary<PriorityLevel, ISpecialsPriority> _specials;
        private IReadOnlyDictionary<PriorityLevel, ISkillsPriority> _skills;
        private IReadOnlyDictionary<PriorityLevel, IResourcesPriority> _resources;

        public IReadOnlyDictionary<PriorityLevel, IMetatypePriority> Metatype
        {
            get
            {
                if (_metatype is null)
                {
                    _metatype = MetatypePrototypes.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IMetatypePriority)kvp.Value);
                }
                return _metatype;
            }
        }

        public IReadOnlyDictionary<PriorityLevel, IAttributesPriority> Attributes
        {
            get
            {
                if (_attributes is null)
                {
                    _attributes = AttributesPrototypes.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IAttributesPriority)kvp.Value);
                }

                return _attributes;
            }
        }

        public IReadOnlyDictionary<PriorityLevel, ISpecialsPriority> Specials
        {
            get
            {
                if (_specials is null)
                {
                    _specials = SpecialsPrototyes.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (ISpecialsPriority)kvp.Value);
                }

                return _specials;
            }
        }

        public IReadOnlyDictionary<PriorityLevel, ISkillsPriority> Skills
        {
            get
            {
                if (_skills is null)
                {
                    _skills = SkillsPrototypes.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (ISkillsPriority)kvp.Value);
                }

                return _skills;
            }
        }

        public IReadOnlyDictionary<PriorityLevel, IResourcesPriority> Resources
        {
            get
            {
                if (_resources is null)
                {
                    _resources = ResourcesPrototypes.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IResourcesPriority)kvp.Value);
                }

                return _resources;
            }
        }

        #endregion

        public void Refresh()
        {
            _attributes = null;
            _metatype = null;
            _resources = null;
            _skills = null;
            _specials = null;
        }

        public PrioritiesPrototype MergeWith(PrioritiesPrototype incomming)
        {
            foreach (var kvp in incomming.AttributesPrototypes)
            {
                AttributesPrototypes[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in incomming.MetatypePrototypes)
            {
                if (!MetatypePrototypes.TryGetValue(kvp.Key, out MetatypePriorityPrototype level))
                {
                    level = new MetatypePriorityPrototype
                    {
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>()
                    };
                    MetatypePrototypes[kvp.Key] = level;
                }
                level._metavariantOptions.AddRange(kvp.Value._metavariantOptions);
            }

            foreach (var kvp in incomming.ResourcesPrototypes)
            {
                ResourcesPrototypes[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in incomming.SkillsPrototypes)
            {
                SkillsPrototypes[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in incomming.SpecialsPrototyes)
            {
                if (!SpecialsPrototyes.TryGetValue(kvp.Key, out var level))
                {
                    level = new SpecialsPriorityPrototype
                    {
                        _options = new List<SpecialOptionPrototype>()
                    };
                    SpecialsPrototyes[kvp.Key] = level;
                }
                level._options.AddRange(kvp.Value._options);
            }

            Refresh();

            return this;
        }
    }
}
