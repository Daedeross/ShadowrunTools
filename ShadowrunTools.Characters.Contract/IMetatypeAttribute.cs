namespace ShadowrunTools.Characters
{
    /// <summary>
    /// For use in <see cref="IMetatype"/>. Represents the limits on an attribute for a particular metatype.
    /// </summary>
    public interface IMetatypeAttribute
    {
        string Name { get; }
        int Min { get; }
        int Max { get; }
    }
}
