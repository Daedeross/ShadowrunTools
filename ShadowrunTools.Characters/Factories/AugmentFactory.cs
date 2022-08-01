namespace ShadowrunTools.Characters.Factories
{
    public class AugmentFactory<T> : IAugmentFactory<T>
        where T : class, INamedItem
    {
        private readonly IDslParser<T> _parser;

        public AugmentFactory(IDslParser<T> parser)
        {
            _parser = parser;
        }

        public IAugment Create(IScope<T> scope, string script)
        {
            var result = _parser.ParseAgument(script, scope);
            if (!result.HasValue)
            {
                return null;
            }

            return new Augment<T>(scope, result.Value.Targets, result.Value.Expression.WatchedProperties, result.Value.Expression.Scoped);
        }
    }
}
