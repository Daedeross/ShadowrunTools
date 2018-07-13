using System;

namespace ShadowrunTools.Characters
{
    public static class RulesExtensions
    {
        public static int AttributeKarma(this IRules rules, int value, int min = 1)
        {
            return rules.AttributeKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int SkillGroupKarma(this IRules rules, int value, int min = 0)
        {
            return rules.SkillGroupKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int ActiveSkillKarma(this IRules rules, int value, int min = 0)
        {
            return rules.ActiveSkillKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int MagicSkillKarma(this IRules rules, int value, int min = 0)
        {
            return rules.MagicSkillKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int ResonanceSkillKarma(this IRules rules, int value, int min = 0)
        {
            return rules.ResonanceSkillKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int KnowledgeSkillKarma(this IRules rules, int value, int min = 0)
        {
            return rules.KnowledgeSkillKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        public static int LanguageSkillKarma(this IRules rules, int value, int min = 0)
        {
            return rules.LanguageSkillKarmaMult * (ValueAt(value) - ValueAt(min));
        }

        /// <summary>
        /// Calulates the Karma cost to Initiate from <para>min</para> to <para>value</para>.
        /// </summary>
        /// <param name="value">The end Initiation level.</param>
        /// <param name="min">The base Initiation level (default 0).</param>
        /// <param name="discounts">The number of discounts to initiation (i.e. Group, Ordeal, and Schooling).</param>
        /// <returns>The total Karma cost to Initiate from <para>min</para> to <para>value</para>.</returns>
        public static int InitiationKarma(this IRules rules, int value, int min = 0, int discounts = 0)
        {
            int val = 0;
            discounts = Math.Min(discounts, 3);
            discounts = Math.Max(discounts, 0);
            for (int i = min + 1; i <= value; i++)
            {
                val += (int)Math.Ceiling((1 - 0.3 * discounts) * (rules.InitiationKarmaBase + rules.InitiationKarmaMult * i));
            }
            return val;
        }

        /// <summary>
        /// Calulates the Karma cost to Submerge from <para>min</para> to <para>value</para>.
        /// </summary>
        /// <param name="value">The end Submersion level.</param>
        /// <param name="min">The base Submersion level (default 0).</param>
        /// <param name="discounts">The number of discounts to initiation (i.e. Group, Ordeal, and Schooling).</param>
        /// <returns>The total Karma cost to Submerge from <para>min</para> to <para>value</para>.</returns>
        public static int SubmersionKarma(this IRules rules, int value, int min = 0, int discounts = 0)
        {
            int val = 0;
            discounts = Math.Min(discounts, 3);
            discounts = Math.Max(discounts, 0);
            for (int i = min + 1; i <= value; i++)
            {
                val += (int)Math.Ceiling((1 - 0.3 * discounts) * (rules.SubmersionKarmaBase + rules.SubmersionKarmaMult * i));
            }
            return val;
        }

        /// <summary>
        /// For use in quadratically scaling Karma calulations (Attributes, skills, Initiation, & Submersion).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int ValueAt(int value)
        {
            return ((value + 1) * value) / 2;
        }
    }
}
