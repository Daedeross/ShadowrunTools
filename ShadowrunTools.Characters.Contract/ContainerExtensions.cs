namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Traits;

    public static class ContainerExtensions
    {
        public static ITraitContainer<ISkill> Skills(this ICategorizedTraitContainer root)
        {
            return (ITraitContainer<ISkill>)root[Categories.Skills];
        }
    }
}
