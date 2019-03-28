﻿using System;
using System.Collections.Generic;
using System.Text;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Foundation;

namespace ShadowrunTools.Characters.Priorities
{
    public class CharacterPriorities : ItemChangedBase, ICharacterPriorities
    {
        private readonly IPriorities _priorities;
        #region ICharacterPriorities Properties

        private PriorityLevel _metatypePriority;
        public PriorityLevel MetatypePriority
        {
            get { return _metatypePriority; }
            set
            {
                if (_metatypePriority != value)
                {
                    SwapPriorities(
                        value,
                        new List<string> { nameof(MetatypePriority), nameof(MetavariantOptions) },
                        ref _metatypePriority);
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
                    SwapPriorities(
                        value,
                        new List<string> { nameof(AttributePriority), nameof(AttributePoints) },
                        ref _attributePriority);
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
                    SwapPriorities(
                        value,
                        new List<string> { nameof(SpecialPriority) },
                        ref _specialPriority);
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
                    SwapPriorities(
                        value,
                        new List<string> { nameof(ResourcePriority), nameof(Resources) },
                        ref _resourcePriority);
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

        #endregion

        public CharacterPriorities(IPriorities priorities)
        {
            _priorities = priorities ?? throw new ArgumentNullException(nameof(priorities));
        }

        private void SwapPriorities(PriorityLevel level, List<string> propertyNames, ref PriorityLevel refValue)
        {
            propertyNames.Add(nameof(TotalPriorityPoints));
            if (_attributePriority == level)
            {
                _attributePriority = refValue;
                propertyNames.AddRange(new[] { nameof(AttributePriority), nameof(AttributePoints) });
            }
            else if (_metatypePriority == level)
            {
                _metatypePriority = refValue;
                propertyNames.AddRange(new[] { nameof(MetatypePriority), nameof(MetavariantOptions) });
            }
            else if (_specialPriority == level)
            {
                _specialPriority = refValue;
                propertyNames.Add(nameof(SpecialPriority));
            }
            else if (_skillPriority == level)
            {
                _skillPriority = refValue;
                propertyNames.AddRange(new[] { nameof(SkillPriority), nameof(SkillPoints), nameof(SkillGroupPoints) });
            }
            else
            {
                _resourcePriority = refValue;
                propertyNames.AddRange(new[] { nameof(ResourcePriority), nameof(Resources) });
            }

            refValue = level;

            RaiseItemChanged(propertyNames.ToArray());
        }
    }
}
