using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public class CharacterPointPriorities : ItemChangedBase, ICharacterPriorities
    {
        private readonly IPriorities _priorities;

        private PriorityLevel _metatypePriority;
        public PriorityLevel MetatypePriority
        {
            get { return _metatypePriority; }
            set
            {
                if (_metatypePriority != value)
                {
                    _metatypePriority = value;
                    RaiseItemChanged(new[] { nameof(MetatypePriority), nameof(MetavariantOptions) });
                }
            }
        }

        private PriorityLevel _attributePriority;
        public PriorityLevel AttributePriority
        {
            get { return _attributePriority; }
            set
            {
                if (_attributePriority != value)
                {
                    _attributePriority = value;
                    RaiseItemChanged(new[] { nameof(AttributePriority), nameof(AttributePoints) });
                }
            }
        }

        private PriorityLevel _specialPriority;
        public PriorityLevel SpecialPriority
        {
            get { return _specialPriority; }
            set
            {
                if (_specialPriority != value)
                {
                    _specialPriority = value;
                    RaiseItemChanged(new[] { nameof(SpecialPriority) });
                }
            }
        }

        private PriorityLevel _skillPriority;
        public PriorityLevel SkillPriority
        {
            get { return _skillPriority; }
            set
            {
                if (_skillPriority != value)
                {
                    _skillPriority = value;
                    RaiseItemChanged(new[] { nameof(SkillPriority), nameof(SkillPoints), nameof(SkillGroupPoints) });
                }
            }
        }

        private PriorityLevel _resourcePriority;
        public PriorityLevel ResourcePriority
        {
            get { return _resourcePriority; }
            set
            {
                if (_resourcePriority != value)
                {
                    _resourcePriority = value;
                    RaiseItemChanged(new[] { nameof(ResourcePriority) });
                }
            }
        }

        public void SetPriorities(PriorityLevel metatype, PriorityLevel attribute,
            PriorityLevel special, PriorityLevel skill, PriorityLevel resource)
        {
            _metatypePriority = metatype;
            _attributePriority = attribute;
            _specialPriority = special;
            _skillPriority = skill;
            _resourcePriority = resource;
            RaiseItemChanged(new[] { nameof(MetatypePriority), nameof(AttributePriority),
                nameof(SpecialPriority), nameof(SkillPriority), nameof(ResourcePriority),
                nameof(AttributePoints), nameof(SkillPoints), nameof(SkillGroupPoints),
                nameof(Resources), nameof(TotalPriorityPoints) });
        }

        public IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions => _priorities.Metatype[_metatypePriority].MetavariantOptions;

        public int AttributePoints => _priorities.Attributes[_attributePriority].AttibutePoints;

        public int SkillPoints => _priorities.Skills[_skillPriority].SkillPoints;

        public int SkillGroupPoints => _priorities.Skills[_skillPriority].SkillGroupPoints;

        public decimal Resources => _priorities.Resources[_resourcePriority].Resources;

        public int TotalPriorityPoints => (int)_attributePriority + (int)_metatypePriority
            + (int)_resourcePriority + (int)_skillPriority + (int)_specialPriority;

        public CharacterPointPriorities(IPriorities priorities)
        {
            _priorities = priorities ?? throw new ArgumentNullException(nameof(priorities));
        }
    }
}
