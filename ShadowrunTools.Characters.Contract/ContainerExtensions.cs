namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Traits;

    public static class ContainerExtensions
    {
        public static ITraitContainer<ISkill> Skills(this ICategorizedTraitContainer root)
        {
            return root[Categories.Skills] as ITraitContainer<ISkill>;
        }
    }
}
