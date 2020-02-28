using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Priorities
{
    public class CharacterPointPriorities : ItemChangedBase, ICharacterPriorities, INotifyValueChanged
    {
        private readonly IPriorities _priorities;

        public event ValueChangedEventHandler ValueChanged;

        private PriorityLevel _metatypePriority;
        public PriorityLevel MetatypePriority
        {
            get { return _metatypePriority; }
            set
            {
                if (this.RaiseAndSetIfValueChanged(ref _metatypePriority, value))
                {
                    _metatypePriority = value;
                    RaiseItemChanged(nameof(MetatypePriority), nameof(MetavariantOptions), nameof(TotalPriorityPoints));
                }
            }
        }

        private PriorityLevel _attributePriority;
        public PriorityLevel AttributePriority
        {
            get { return _attributePriority; }
            set
            {
                if (this.RaiseAndSetIfValueChanged(ref _attributePriority, value))
                {
                    _attributePriority = value;
                    RaiseItemChanged(nameof(AttributePriority), nameof(AttributePoints), nameof(TotalPriorityPoints));
                }
            }
        }

        private PriorityLevel _specialPriority;
        public PriorityLevel SpecialPriority
        {
            get { return _specialPriority; }
            set
            {
                if (this.RaiseAndSetIfValueChanged(ref _specialPriority, value))
                {
                    _specialPriority = value;
                    RaiseItemChanged(nameof(SpecialPriority), nameof(TotalPriorityPoints));
                }
            }
        }

        private PriorityLevel _skillPriority;
        public PriorityLevel SkillPriority
        {
            get { return _skillPriority; }
            set
            {
                if (this.RaiseAndSetIfValueChanged(ref _skillPriority, value))
                {
                    _skillPriority = value;
                    RaiseItemChanged(nameof(SkillPriority), nameof(SkillPoints), nameof(SkillGroupPoints), nameof(TotalPriorityPoints));
                }
            }
        }

        private PriorityLevel _resourcePriority;
        public PriorityLevel ResourcePriority
        {
            get { return _resourcePriority; }
            set
            {
                if (this.RaiseAndSetIfValueChanged(ref _resourcePriority, value))
                {
                    _resourcePriority = value;
                    RaiseItemChanged(nameof(ResourcePriority), nameof(TotalPriorityPoints));
                }
            }
        }

        public void SetPriorities(PriorityLevel metatype, PriorityLevel attribute,
            PriorityLevel special, PriorityLevel skill, PriorityLevel resource)
        {
            var propertyNames = new List<string>{ nameof(ResourcePriority) };

            if (this.RaiseAndSetIfValueChanged(ref _metatypePriority, metatype, nameof(MetatypePriority)))
            {
                propertyNames.Add(nameof(MetatypePriority));
            }
            if (this.RaiseAndSetIfValueChanged(ref _attributePriority, attribute, nameof(AttributePriority)))
            {
                propertyNames.Add(nameof(AttributePriority));
                propertyNames.Add(nameof(AttributePoints));
            }
            if (this.RaiseAndSetIfValueChanged(ref _specialPriority, special, nameof(SpecialPriority)))
            {
                propertyNames.Add(nameof(SpecialPriority));
            }
            if (this.RaiseAndSetIfValueChanged(ref _skillPriority, skill, nameof(SkillPriority)))
            {
                propertyNames.Add(nameof(SkillPriority));
                propertyNames.Add(nameof(SkillPoints));
                propertyNames.Add(nameof(SkillGroupPoints));
            }
            if (this.RaiseAndSetIfValueChanged(ref _resourcePriority, resource, nameof(ResourcePriority)))
            {
                propertyNames.Add(nameof(ResourcePriority));
                propertyNames.Add(nameof(Resources));
            }

            RaiseItemChanged(propertyNames.ToArray());
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

        public void RaiseValueChanged(ValueChangedEventArgs args)
        {
            ValueChanged?.Invoke(this, args);
        }
    }
}
