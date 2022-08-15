using ShadowrunTools.Characters.Contract.Model;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Characters.ViewModels.Internal
{
    public static class SkillHelpers
    {
        public static Func<ISkill, bool> IsActiveSkill = (skill) => skill.SkillType == SkillType.Active || skill.SkillType == SkillType.Magical || skill.SkillType == SkillType.Resonance;
        public static Func<ISkill, bool> IsKnowledgeSkill = (skill) => skill.SkillType == SkillType.Knowledge || skill.SkillType == SkillType.Language;

        public static IReadOnlyDictionary<string, Func<ISkill, bool>> SharedFilters = new Dictionary<string, Func<ISkill, bool>>
        {
            { "Show All"        , s => true },
            { "Rating > 0"      , s => s.AugmentedRating > 0 },
            { "Total Rating > 0", s => s.AugmentedPool > 0 },
            { "Rating = 0"      , s => s.AugmentedRating == 0 },
            { "Attribute: Body"     , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Body) },
            { "Attribute: Agility"  , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Agility) },
            { "Attribute: Reaction" , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Reaction) },
            { "Attribute: Strength" , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Strength) },
            { "Attribute: Willpower", s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Willpower) },
            { "Attribute: Logic"    , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Logic) },
            { "Attribute: Intuition", s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Intuition) },
            { "Attribute: Charisma" , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Charisma) },
            { "Attribute: Edge"     , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Edge) },
            { "Attribute: Essense"  , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Essense) },
            { "Attribute: Magic"    , s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Magic) },
            { "Attribute: Resonance", s => string.Equals(s.LinkedAttribute?.Name, AttributeNames.Resonance) },
        };

        public static IReadOnlyDictionary<string, Func<ISkill, bool>> ActiveSkillFilters = new Dictionary<string, Func<ISkill, bool>>
        {
            { "Un-Grouped"      , s => string.IsNullOrWhiteSpace(s.GroupName) },
            //{ "In Broken Skill Group", s => string.IsNullOrWhiteSpace(s.GroupName) },  // TODO: Implement skill groups
            { "Category: Combat Active"        , s => string.Equals(s.SubCategory, SkillCategories.CombatActive) },
            { "Category: Physical Active"      , s => string.Equals(s.SubCategory, SkillCategories.PhysicalActive) },
            { "Category: Social Active"        , s => string.Equals(s.SubCategory, SkillCategories.SocialActive) },
            { "Category: Magical Active"       , s => string.Equals(s.SubCategory, SkillCategories.MagicalActive) },
            { "Category: Pseudo-Magical Active", s => string.Equals(s.SubCategory, SkillCategories.PseudoMagicalActive) },
            { "Category: Resonance Active"     , s => string.Equals(s.SubCategory, SkillCategories.ResonanceActive) },
            { "Category: Technical Active"     , s => string.Equals(s.SubCategory, SkillCategories.TechnicalActive) },
            { "Category: Vehicle Active"       , s => string.Equals(s.SubCategory, SkillCategories.VehicleActive) },
        }.Concat(SharedFilters)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        public static IReadOnlyDictionary<string, Func<ISkill, bool>> KnowledgeSkillFilters = new Dictionary<string, Func<ISkill, bool>>
        {
            { "Category: Academic Knowledge"    , s => string.Equals(s.SubCategory, SkillCategories.AcademicKnowledge) },
            { "Category: Interest Knowledge"    , s => string.Equals(s.SubCategory, SkillCategories.InterestKnowledge) },
            { "Category: Professional Knowledge", s => string.Equals(s.SubCategory, SkillCategories.ProfessionalKnowledge) },
            { "Category: Street Knowledge"      , s => string.Equals(s.SubCategory, SkillCategories.StreetKnowledge) },
            { "Category: Language"              , s => string.Equals(s.SubCategory, SkillCategories.Language) },
        }.Concat(SharedFilters)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
