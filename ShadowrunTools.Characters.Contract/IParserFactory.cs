namespace ShadowrunTools.Characters
{
    public interface IParserFactory
    {
        IDslParser<T> Create<T>();

        void Release(object parser);
    }
}
