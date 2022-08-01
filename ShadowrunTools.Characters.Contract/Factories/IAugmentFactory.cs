namespace ShadowrunTools.Characters.Factories
{
    public interface IAugmentFactory<T>: IFactory
        where T : class, INamedItem
    {
        public IAugment Create(IScope<T> scope, string script);
    }
}
