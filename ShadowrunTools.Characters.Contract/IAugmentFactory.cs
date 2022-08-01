namespace ShadowrunTools.Characters
{
    public interface IAugmentFactory<T>
        where T : class, INamedItem
    {
        public IAugment Create(IScope<T> scope, string script);
    }
}
