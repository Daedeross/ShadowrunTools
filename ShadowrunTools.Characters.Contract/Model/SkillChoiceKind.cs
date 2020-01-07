namespace ShadowrunTools.Characters.Model
{
    public enum SkillChoiceKind
    {
        None = 0,
        Skill = 1,
        Group = 1 << 1,
        Category = 1 << 2
    }
}
