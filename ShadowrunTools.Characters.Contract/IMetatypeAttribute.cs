namespace ShadowrunTools.Characters
{
    public interface IMetatypeAttribute
    {
        string Name { get; }
        int Min { get; }
        int Max { get; }
    }
}
