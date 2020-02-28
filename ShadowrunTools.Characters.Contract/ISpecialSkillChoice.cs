namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;

    public interface ISpecialSkillChoice
    {
        public int Rating { get; set; }
        public int Count { get; set; }
        public SkillChoiceKind Kind { get; set; }
        public string Choice { get; set; }
    }
}
