namespace ShadowrunTools.Characters.Internal
{
    public class Scope<T> : IScope<T>
        where T : class, INamedItem
    {
        public T Owner { get; init; }

        public T Me { get; init; }

        public ICategorizedTraitContainer Traits { get; init; }

        public Scope(T owner, T me, ICategorizedTraitContainer traits)
        {
            Owner = owner;
            Me = me;
            Traits = traits;
        }
    }
}
