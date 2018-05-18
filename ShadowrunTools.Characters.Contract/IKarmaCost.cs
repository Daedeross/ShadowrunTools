namespace ShadowrunTools.Characters.Contract
{
    /// <summary>
    /// Traits that implement this interface can be improved or bought with Karma
    /// </summary>
    public interface IKarmaCost
    {
        /// <summary>
        /// The Karma cost of the trait or item.
        /// </summary>
        int Karma { get; }
    }
}
