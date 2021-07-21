namespace ShadowrunTools.Characters
{
    /// <summary>
    /// An metatype as applied to a character.
    /// </summary>
    public interface ICharacterMetatype : INotifyItemChanged
    {
        /// <summary>
        /// The name of the Metatype/Metavariant
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indexer to get an <see cref="IMetatypeAttribute"/> by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IMetatypeAttribute this[string name] { get; }

        /// <summary>
        /// Safe method to retrieve an <see cref="IMetatypeAttribute"/> by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns>True if there exists an attribute by that name.</returns>
        bool TryGetAttribute(string name, out IMetatypeAttribute attribute);
    }
}
