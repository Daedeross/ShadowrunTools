using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PriorityRow : ViewModelBase, IPriorityRow
    {
        private readonly IPriorities _priorities;
        private readonly ICharacterPriorities _characterPriorities;

        public PriorityLevel Level { get; private set; }

        public IPriorityCell Metatype { get; set; }

        public IPriorityCell Attributes { get; set; }

        public IPriorityCell Specials { get; set; }

        public IPriorityCell Skills { get; set; }

        public IPriorityCell Resources { get; set; }

        public PriorityRow(DisplaySettings displaySettings,
            PriorityLevel level,
            IPriorities priorities,
            ICharacterPriorities characterPriorities)
            : base(displaySettings)
        {
            _priorities = priorities;
            _characterPriorities = characterPriorities;
            Level = level;

            Metatype = new PriorityCell(displaySettings, GetMetatypeItems(level, priorities), OnMetatypeChanged,
                isSelected: _characterPriorities.MetatypePriority == level);
            Attributes = new PriorityCell(displaySettings, GetAttributesItems(level, priorities), OnAttributeChanged,
                isSelected: _characterPriorities.AttributePriority == level);
            Specials = new PriorityCell(displaySettings, GetSpecialsItems(level, priorities), OnSpecialChanged,
                isSelected: _characterPriorities.SpecialPriority == level);
            Skills = new PriorityCell(displaySettings, GetSkillsItems(level, priorities), OnSkillChanged,
                isSelected: _characterPriorities.SkillPriority == level);
            Resources = new PriorityCell(displaySettings, GetResourcesItems(level, priorities), OnResourceChanged,
                isSelected: _characterPriorities.ResourcePriority == level);

            var temp = _characterPriorities as INotifyValueChanged;
            if (temp != null)
            {
                temp.ValueChanged += OnCharacterPrioritiesChanged;
            }
        }

        private void OnCharacterPrioritiesChanged(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue is PriorityLevel newValue
                && e.OldValue is PriorityLevel oldValue
                && (newValue == Level || oldValue == Level))
            {
                switch (e.PropertyName)
                {
                    case nameof(ICharacterPriorities.MetatypePriority):
                        Metatype.IsSelected = newValue == Level;
                        break;
                    case nameof(ICharacterPriorities.AttributePriority):
                        Attributes.IsSelected = newValue == Level;
                        break;
                    case nameof(ICharacterPriorities.SpecialPriority):
                        Specials.IsSelected = newValue == Level;
                        break;
                    case nameof(ICharacterPriorities.SkillPriority):
                        Skills.IsSelected = newValue == Level;
                        break;
                    case nameof(ICharacterPriorities.ResourcePriority):
                        Resources.IsSelected = newValue == Level;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnMetatypeChanged(bool isSelected)
        {
            if (isSelected)
            {
                _characterPriorities.MetatypePriority = Level;
            }
        }

        private void OnAttributeChanged(bool isSelected)
        {
            if (isSelected)
            {
                _characterPriorities.AttributePriority = Level; 
            }
        }

        private void OnSpecialChanged(bool isSelected)
        {
            if (isSelected)
            {
                _characterPriorities.SpecialPriority = Level;
            }
        }

        private void OnSkillChanged(bool isSelected)
        {
            if (isSelected)
            {
                _characterPriorities.SkillPriority = Level;
            }
        }

        private void OnResourceChanged(bool isSelected)
        {
            if (isSelected)
            {
                _characterPriorities.ResourcePriority = Level;
            }
        }

        private static List<string> GetMetatypeItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Metatype.TryGetValue(level, out var metas))
            {
                return new List<string>();
            }

            return metas.MetavariantOptions.Select(opt => opt.AdditionalKarmaCost == 0
                ? $"{opt.Metavariant}-{opt.SpecialAttributePoints}"
                : $"{opt.Metavariant}-{opt.SpecialAttributePoints} (-{opt.AdditionalKarmaCost})")
                .ToList();
        }

        private static List<string> GetAttributesItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Attributes.TryGetValue(level, out var attrs))
            {
                return new List<string>();
            }

            return new List<string> { attrs.AttibutePoints.ToString() };
        }

        private static List<string> GetSpecialsItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Specials.TryGetValue(level, out var specials))
            {
                return new List<string>();
            }

            return specials.Options
                .Select(o => GetSpecialText(o))
                .ToList();
        }

        private static List<string> GetSkillsItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Skills.TryGetValue(level, out var skills))
            {
                return new List<string>();
            }

            return new List<string> { $"{skills.SkillPoints}/{skills.SkillGroupPoints}" };
        }

        private static List<string> GetResourcesItems(PriorityLevel level, IPriorities priorities)
        {
            if (!priorities.Resources.TryGetValue(level, out var resources))
            {
                return new List<string>();
            }

            return new List<string> { resources.Resources.ToString() };
        }

        private static string GetSpecialText(ISpecialOption specialOption)
        {
            var builder = new StringBuilder(specialOption.Quality);
            builder.Append(": ");
            builder.Append(specialOption.AttributeName);
            builder.Append(' ');
            builder.Append(specialOption.AttributeRating);

            if (specialOption.SkillOptions?.Count > 0)
            {
                foreach (var option in specialOption.SkillOptions)
                {
                    builder.Append(", ");
                    builder.Append(IntToToWord(option.Count));
                    builder.Append(" Rating ");
                    builder.Append(option.Rating);
                    builder.Append(" ");
                    builder.Append(option.Choice);
                    builder.Append(" skill");
                    if (option.Count > 1)
                    {
                        builder.Append('s');
                    }
                }
            }
            if (specialOption.SkillGroupOptions?.Count > 0)
            {
                foreach (var option in specialOption.SkillGroupOptions)
                {
                    builder.Append(", ");
                    builder.Append(IntToToWord(option.Count));
                    builder.Append(" Rating ");
                    builder.Append(option.Rating);
                    builder.Append(" ");
                    builder.Append(option.Choice);
                    builder.Append(" skill group");
                    if (option.Count > 1)
                    {
                        builder.Append('s');
                    }
                }
            }
            if (specialOption.FreeSpells > 0)
            {
                builder.Append(", ");
                builder.Append(specialOption.FreeSpells);
                builder.Append(" spell");
                if (specialOption.FreeSpells > 1)
                {
                    builder.Append('s');
                }
            }
            if (specialOption.FreeComplexForms > 0)
            {
                builder.Append(", ");
                builder.Append(specialOption.FreeComplexForms);
                builder.Append(" complex form");
                if (specialOption.FreeComplexForms > 1)
                {
                    builder.Append('s');
                }
            }

            return builder.ToString();
        }

        private static string[] Digits =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "sixteen",
            "seventeen",
            "nineteen",
            "twenty"
        };

        private static string IntToToWord(int i)
        {
            if (i < 21 && i >=0 )
            {
                return Digits[i];
            }
            throw new InvalidOperationException();
        }
    }
}
