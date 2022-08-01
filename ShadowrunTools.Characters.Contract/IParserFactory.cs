namespace ShadowrunTools.Characters
{
    public interface IParserFactory
    {
        IDslParser<T> Create<T>()
            where T : class, INamedItem;

        void Release(object parser);
    }
}
