namespace ShadowrunTools.Characters.Factories
{
    public interface IParserFactory : IFactory
    {
        IDslParser<T> Create<T>()
            where T : class, INamedItem;

        void Release(object parser);
    }
}
