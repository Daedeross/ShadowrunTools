namespace ShadowrunTools.Characters
{
    public interface ISelfValidate
    {
        bool Invalid { get; }
        string InvalidMessage { get; }
    }
}
